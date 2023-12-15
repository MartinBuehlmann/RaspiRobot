namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.Common.DependencyInjection;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.Settings;
using State = RaspiRobot.RobotControl.Devices.Robot.State;

internal class GrabItRobot : IRobot, IStartableDevice, IShutdownableDevice
{
    private readonly IGrabItDriver driver;
    private readonly RobotSettings settings;
    private readonly ISettingsRetriever settingsRetriever;
    private readonly TransportSequenceBuilder transportSequenceBuilder;
    private readonly TransportSequenceExecutor transportSequenceExecutor;
    private readonly List<IRobotStateNotifier> robotStateNotifiers;

    public GrabItRobot(
        RobotSettings settings,
        ISettingsRetriever settingsRetriever,
        TransportSequenceBuilder transportSequenceBuilder,
        TransportSequenceExecutor transportSequenceExecutor,
        IGrabItDriver driver,
        Factory factory)
    {
        this.settings = settings;
        this.settingsRetriever = settingsRetriever;
        this.transportSequenceBuilder = transportSequenceBuilder;
        this.transportSequenceExecutor = transportSequenceExecutor;
        this.driver = driver;
        this.MdiRobot = factory.Create<IMdiRobot>(this.driver);
        this.robotStateNotifiers = new List<IRobotStateNotifier>();
    }

    public IMdiRobot MdiRobot { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.driver.Initialize();
        RobotSettings robotSettings = await this.settingsRetriever.RetrieveRobotSettingsAsync();
        await this.ExecuteSequencesAsync(new[] { robotSettings.HomingSequence });
    }

    public Task ShutdownAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken)
    {
        this.robotStateNotifiers.Add(robotStateNotifier);
        await robotStateNotifier.NotifyAsync(State.Ready);
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
        StoragePlace? destinationPlaceForPalletOnChuck)
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

        await this.ExecuteSequencesAsync(sequences);

        return new SuccessResponse();
    }

    public async Task<ICommandResponse> UnloadChuckAsync(
        MachineChuck chuck,
        StoragePlace destinationPlace)
    {
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.UnloadChuckSequence(
            await this.settingsRetriever.RetrieveByAsync(chuck),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            await this.settingsRetriever.RetrieveRobotSettingsAsync());

        await this.ExecuteSequencesAsync(sequences);

        return new SuccessResponse();
    }

    public async Task<ICommandResponse> ExchangePlaceAsync(StoragePlace sourcePlace, StoragePlace destinationPlace)
    {
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.ExchangePlaceSequence(
            await this.settingsRetriever.RetrieveByAsync(sourcePlace),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            await this.settingsRetriever.RetrieveRobotSettingsAsync());

        await this.ExecuteSequencesAsync(sequences);

        return await Task.FromResult(new SuccessResponse());
    }

    private async Task ExecuteSequencesAsync(IReadOnlyList<Sequence> sequences)
    {
        await this.NotifyStateAsync(State.Busy);
        this.transportSequenceExecutor.Execute(sequences, this.driver);
        await this.NotifyStateAsync(State.Ready);
    }

    private async Task NotifyStateAsync(State state)
    {
        foreach (IRobotStateNotifier robotStateNotifier in this.robotStateNotifiers)
        {
            await robotStateNotifier.NotifyAsync(state);
        }
    }
}