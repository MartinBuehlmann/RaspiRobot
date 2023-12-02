namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.Settings;

// TODO: Implement
internal class GrabItRobot : IRobot, IStartableDevice, IShutdownableDevice
{
    private readonly IGrabItDriver driver;
    private readonly RobotSettings settings;
    private readonly ISettingsRetriever settingsRetriever;
    private readonly TransportSequenceBuilder transportSequenceBuilder;

    public GrabItRobot(
        RobotSettings settings,
        ISettingsRetriever settingsRetriever,
        TransportSequenceBuilder transportSequenceBuilder,
        IGrabItDriver driver)
    {
        this.settings = settings;
        this.settingsRetriever = settingsRetriever;
        this.transportSequenceBuilder = transportSequenceBuilder;
        this.driver = driver;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.driver.Initialize();
        return Task.CompletedTask;
    }

    public Task ShutdownAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken)
    {
        // TODO: Replace with real implementation
        await robotStateNotifier.NotifyAsync(RobotControl.Devices.Robot.State.Ready);
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken)
    {
        // TODO: Replace with real implementation
        await alarmsNotifier.NotifyAsync(ReadOnlyList.Empty<Alarm>());
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForChuckLoadingsChangedAsync(
        IChuckLoadingsNotifier chuckLoadingsNotifier,
        CancellationToken cancellationToken)
    {
        // TODO: Replace with real implementation
        await chuckLoadingsNotifier.NotifyAsync(ReadOnlyList.Empty<ChuckLoading>());
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task<ICommandResponse> LoadChuckAsync(
        StoragePlace sourcePlace,
        MachineChuck chuck,
        StoragePlace? destinationPlaceForPalletOnChuck)
    {
        // TODO:
        //  - State busy
        //  - Retrieve place and chuck settings
        //  - Use TransportSequenceBuilder to create required sequence(s)
        //  - State ready
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

        this.driver.Execute(sequences);

        return new SuccessResponse();
    }

    public async Task<ICommandResponse> UnloadChuckAsync(
        MachineChuck chuck,
        StoragePlace destinationPlace)
    {
        // TODO:
        //  - State busy
        //  - Retrieve place and chuck settings
        //  - Use TransportSequenceBuilder to create required sequence(s)
        //  - State ready
        IReadOnlyList<Sequence> sequences = this.transportSequenceBuilder.UnloadChuckSequence(
            await this.settingsRetriever.RetrieveByAsync(chuck),
            await this.settingsRetriever.RetrieveByAsync(destinationPlace),
            await this.settingsRetriever.RetrieveRobotSettingsAsync());

        this.driver.Execute(sequences);

        return new SuccessResponse();
    }

    public async Task<ICommandResponse> MovePalletAsync(
        StoragePlace sourcePlace,
        StoragePlace destinationPlace)
    {
        // TODO:
        //  - State busy
        //  - Retrieve place settings
        //  - Use TransportSequenceBuilder to create required sequence(s)
        //  - State ready
        return await Task.FromResult(new SuccessResponse());
    }
}