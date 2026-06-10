namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.DependencyInjection;
using Common.Logging;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Connection;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Robot.State;
using RaspiRobot.RobotControl.Devices.Robot.Steps;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.State;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.Settings;

internal class GrabItRobot : IRobot, IStartableDevice, IShutdownableDevice
{
    private readonly RobotSettings robotSettings;
    private readonly ISettingsRetriever settingsRetriever;
    private readonly TransportSequenceBuilder transportSequenceBuilder;
    private readonly TransportSequenceExecutor transportSequenceExecutor;
    private readonly IGrabItDriver driver;
    private readonly IOperationModeRetriever operationModeRetriever;
    private readonly RobotStateCache robotStateCache;
    private readonly Log logger;

    public GrabItRobot(
        RobotSettings robotSettings,
        ISettingsRetriever settingsRetriever,
        TransportSequenceBuilder transportSequenceBuilder,
        TransportSequenceExecutor transportSequenceExecutor,
        IGrabItDriver driver,
        IOperationModeRetriever operationModeRetriever,
        RobotStateCache robotStateCache,
        Factory factory,
        Log logger)
    {
        this.robotSettings = robotSettings;
        this.settingsRetriever = settingsRetriever;
        this.transportSequenceBuilder = transportSequenceBuilder;
        this.transportSequenceExecutor = transportSequenceExecutor;
        this.driver = driver;
        this.operationModeRetriever = operationModeRetriever;
        this.robotStateCache = robotStateCache;
        this.logger = logger;
        this.MdiRobot = factory.Create<IMdiRobot>(this.driver);
        this.Alarms = factory.Create<IAlarmsFacade>();
        this.Alarms.AddAlarm(new AlarmData("1", "Emergency Stop", Severity.Error, DateTimeOffset.UtcNow, false));
        this.Alarms.AddAlarm(new AlarmData("2", "Position error axis 1", Severity.Error, DateTimeOffset.UtcNow, false));
        this.Alarms.AddAlarm(new AlarmData("10", "Annual maintenance overdue", Severity.Warning, DateTimeOffset.UtcNow, false));
        this.Alarms.AddAlarm(new AlarmData("20", "Update available", Severity.Information, DateTimeOffset.UtcNow, false));
    }

    public IMdiRobot MdiRobot { get; }

    public IAlarmsFacade Alarms { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.driver.Initialize();
        this.InitializeState();
        await this.ExecuteSequencesAsync(
            this.transportSequenceBuilder.HomingSequence(this.robotSettings),
            CancellationToken.None);
    }

    public Task ShutdownAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public IReadOnlyList<PositionSettings> RetrieveAxisPositions()
    {
        return this.driver.CurrentDrivePositions
            .ToList()
            .Select(x => new PositionSettings(x.Key, x.Value))
            .OrderBy(x => x.Drive)
            .ToList();
    }

    public async Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken)
    {
        await robotStateNotifier.NotifyAsync(RobotState.Ready);
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken)
    {
        await alarmsNotifier.NotifyAsync(this.Alarms.RetrieveAlarms().Where(x => x.IsActive).ToArray());

        // TODO: Extend to listen for alarms changes and continuously notify the alarms notifier.
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForChuckLoadingsChangedAsync(
        int[] chuckNumbers,
        IChuckOccupancyNotifier chuckOccupancyNotifier,
        CancellationToken cancellationToken)
    {
        // As long as there is no real tracking, we just return an empty chuck loading and wait.
        await chuckOccupancyNotifier.NotifyAsync(ReadOnlyList.Empty<ChuckOccupancy>());
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task<ICommandResponse> LoadChuckAsync(
        StoragePlace sourcePlace,
        MachineChuck chuck,
        StoragePlace? destinationPlaceForPalletOnChuck,
        CancellationToken rollbackCancellationToken)
    {
        var sequences = new List<Sequence>();
        ChuckSettings chuckSettings = await this.settingsRetriever.RetrieveByAsync(chuck);
        if (destinationPlaceForPalletOnChuck is not null)
        {
            sequences.AddRange(
                this.transportSequenceBuilder.UnloadChuckSequence(
                    chuckSettings,
                    await this.settingsRetriever.RetrieveByAsync(destinationPlaceForPalletOnChuck),
                    this.robotSettings));
        }

        sequences.AddRange(
            this.transportSequenceBuilder.LoadChuckSequence(
                await this.settingsRetriever.RetrieveByAsync(sourcePlace),
                chuckSettings,
                this.robotSettings));

        return await this.ExecuteSequencesAsync(sequences, rollbackCancellationToken);
    }

    public async Task<ICommandResponse> UnloadChuckAsync(
        MachineChuck chuck,
        StoragePlace destinationPlace,
        CancellationToken rollbackCancellationToken)
    {
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.UnloadChuckSequence(
            await this.settingsRetriever.RetrieveByAsync(chuck),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            this.robotSettings);

        return await this.ExecuteSequencesAsync(sequences, rollbackCancellationToken);
    }

    public async Task<ICommandResponse> ExchangeStoragePlaceAsync(
        StoragePlace sourcePlace,
        StoragePlace destinationPlace)
    {
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.ExchangePlaceSequence(
            await this.settingsRetriever.RetrieveByAsync(sourcePlace),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            this.robotSettings);

        return await this.ExecuteSequencesAsync(sequences, CancellationToken.None);
    }

    public async Task SubscribeForConnectionStateChangedAsync(
        IConnectionStateNotifier connectionStateNotifier,
        CancellationToken cancellationToken)
    {
        await connectionStateNotifier.NotifyAsync(true);
        cancellationToken.WaitHandle.WaitOne();
    }

    private void InitializeState()
    {
        var newRobotState = RobotState.NotReady;
        if (this.IsRobotInAutomaticMode())
        {
            if (this.HasRobotAlarms())
            {
                newRobotState = RobotState.Error;
            }
            else
            {
                newRobotState = RobotState.Ready;
            }
        }

        this.robotStateCache.SetRobotState(newRobotState);
    }

    private bool HasRobotAlarms()
    {
        // TODO: Alarms not implemented yet.
        return false;
    }

    private bool IsRobotInAutomaticMode()
    {
        return this.operationModeRetriever.OperationMode ==
               RobotControl.Devices.Robot.OperationMode.OperationMode.Automatic;
    }

// TODO: Handle rollbackCancellationToken
// Configure for each step if rollback is supported.
// If yes, stop execution and a higher instance needs to care about the rollback actions - if no, just continue.
    private async Task<ICommandResponse> ExecuteSequencesAsync(
        IReadOnlyList<Sequence> sequences,
        CancellationToken rollbackCancellationToken)
    {
        if (this.robotStateCache.RobotState == RobotState.Ready)
        {
            rollbackCancellationToken.Register(() =>
                this.logger.Info(
                    "Rollback of the current transport sequence has been detected, but is currently not implemented so the current transport sequence will continue to execute"));
            this.NotifyState(RobotState.Busy);
            await this.transportSequenceExecutor.ExecuteAsync(sequences, this.driver);
            this.NotifyState(RobotState.Ready);
            return new SuccessResponse();
        }

        return new ErrorResponse("Unable to execute a robot command when robot is not in Automatic mode.");
    }

    private void NotifyState(RobotState state)
    {
        this.robotStateCache.SetRobotState(state);
    }
}