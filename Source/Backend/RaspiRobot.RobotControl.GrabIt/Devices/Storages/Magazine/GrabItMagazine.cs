namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;

using System.Threading;
using System.Threading.Tasks;
using Common;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Connection;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.State;

internal class GrabItMagazine : IMagazine
{
    private readonly MagazineSettings settings;

    public GrabItMagazine(MagazineSettings settings)
    {
        this.settings = settings;
    }

    public string Identifier => this.settings.Identifier;

    public async Task SubscribeForStateChangedAsync(
        IStorageStateNotifier magazineStateNotifier,
        CancellationToken cancellationToken)
    {
        await magazineStateNotifier.NotifyAsync(State.Ready);
        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    public async Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken)
    {
        await alarmsNotifier.NotifyAsync(ReadOnlyList.Empty<Alarm>());
        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    public async Task SubscribeForConnectionStateChangedAsync(
        IConnectionStateNotifier connectionStateNotifier,
        CancellationToken cancellationToken)
    {
        await connectionStateNotifier.NotifyAsync(true);
        await Task.Delay(Timeout.Infinite, cancellationToken);
    }
}