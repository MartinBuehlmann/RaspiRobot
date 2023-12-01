namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Settings;

// TODO: Implement
internal class GrabItRobot : IRobot, IStartableDevice, IShutdownableDevice
{
    private readonly IGrabItDriver driver;
    private readonly RobotSettings settings;

    public GrabItRobot(
        RobotSettings settings,
        IGrabItDriver driver)
    {
        this.settings = settings;
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

    public ICommandResponse LoadChuck(
        StoragePlace sourcePlace,
        MachineChuck chuck,
        StoragePlace? destinationPlaceForPalletOnChuck)
    {
        // TODO: Fake with State busy - Wait 5s - State ready =>too many state transitions if unload is part of load
        return new SuccessResponse();
    }

    public ICommandResponse UnloadChuck(
        MachineChuck chuck,
        StoragePlace destinationPlace)
    {
        // TODO: Fake with State busy - Wait 5s - State ready
        return new SuccessResponse();
    }

    public ICommandResponse MovePallet(
        StoragePlace sourcePlace,
        StoragePlace destinationPlace)
    {
        // TODO: Fake with State busy - Wait 5s - State ready
        return new SuccessResponse();
    }
}