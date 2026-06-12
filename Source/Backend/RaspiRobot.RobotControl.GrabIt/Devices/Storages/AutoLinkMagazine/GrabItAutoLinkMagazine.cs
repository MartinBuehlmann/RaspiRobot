namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;

using System.Threading;
using System.Threading.Tasks;
using Common;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Connection;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.State;

internal class GrabItAutoLinkMagazine : IAutoLinkMagazine
{
    private readonly AutoLinkMagazineSettings settings;

    public GrabItAutoLinkMagazine(AutoLinkMagazineSettings settings)
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