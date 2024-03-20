namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

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
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;
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
    private readonly RobotSettings settings;
    private readonly ISettingsRetriever settingsRetriever;
    private readonly TransportSequenceBuilder transportSequenceBuilder;
    private readonly TransportSequenceExecutor transportSequenceExecutor;
    private readonly IGrabItDriver driver;
    private readonly IOperationModeRetriever operationModeRetriever;
    private readonly RobotStateCache robotStateCache;
    private readonly Log logger;
    private readonly List<IRobotStateNotifier> robotStateNotifiers;

    public GrabItRobot(
        RobotSettings settings,
        ISettingsRetriever settingsRetriever,
        TransportSequenceBuilder transportSequenceBuilder,
        TransportSequenceExecutor transportSequenceExecutor,
        IGrabItDriver driver,
        IOperationModeRetriever operationModeRetriever,
        RobotStateCache robotStateCache,
        Factory factory,
        Log logger)
    {
        this.settings = settings;
        this.settingsRetriever = settingsRetriever;
        this.transportSequenceBuilder = transportSequenceBuilder;
        this.transportSequenceExecutor = transportSequenceExecutor;
        this.driver = driver;
        this.operationModeRetriever = operationModeRetriever;
        this.robotStateCache = robotStateCache;
        this.logger = logger;
        this.MdiRobot = factory.Create<IMdiRobot>(this.driver);
        this.robotStateNotifiers = new List<IRobotStateNotifier>();
    }

    public IMdiRobot MdiRobot { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.driver.Initialize();
        RobotSettings robotSettings = await this.settingsRetriever.RetrieveRobotSettingsAsync();
        this.InitializeState();
        await this.ExecuteSequencesAsync(
            this.transportSequenceBuilder.HomingSequence(robotSettings),
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
        this.robotStateNotifiers.Add(robotStateNotifier);
        await robotStateNotifier.NotifyAsync(RobotState.Ready);
        cancellationToken.WaitHandle.WaitOne();
        this.robotStateNotifiers.Remove(robotStateNotifier);
    }

    public async Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken)
    {
        await alarmsNotifier.NotifyAsync(ReadOnlyList.Empty<Alarm>());
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForChuckLoadingsChangedAsync(
        int[] chuckNumbers,
        IChuckLoadingsNotifier chuckLoadingsNotifier,
        CancellationToken cancellationToken)
    {
        // As long as there is no real tracking, we just return an empty chuck loading and wait.
        await chuckLoadingsNotifier.NotifyAsync(ReadOnlyList.Empty<ChuckLoading>());
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
        RobotSettings robotSettings = await this.settingsRetriever.RetrieveRobotSettingsAsync();
        if (destinationPlaceForPalletOnChuck is not null)
        {
            sequences.AddRange(
                this.transportSequenceBuilder.UnloadChuckSequence(
                    chuckSettings,
                    await this.settingsRetriever.RetrieveByAsync(destinationPlaceForPalletOnChuck),
                    robotSettings));
        }

        sequences.AddRange(
            this.transportSequenceBuilder.LoadChuckSequence(
                await this.settingsRetriever.RetrieveByAsync(sourcePlace),
                chuckSettings,
                robotSettings));

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
            await this.settingsRetriever.RetrieveRobotSettingsAsync());

        return await this.ExecuteSequencesAsync(sequences, rollbackCancellationToken);
    }

    public async Task<ICommandResponse> ExchangePlaceAsync(StoragePlace sourcePlace, StoragePlace destinationPlace)
    {
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.ExchangePlaceSequence(
            await this.settingsRetriever.RetrieveByAsync(sourcePlace),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            await this.settingsRetriever.RetrieveRobotSettingsAsync());

        return await this.ExecuteSequencesAsync(sequences, CancellationToken.None);
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