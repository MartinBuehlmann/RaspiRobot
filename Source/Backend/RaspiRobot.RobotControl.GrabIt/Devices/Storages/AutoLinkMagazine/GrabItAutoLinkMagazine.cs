namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;

using System.Threading;
using System.Threading.Tasks;
using Common;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;

internal class GrabItAutoLinkMagazine : IAutoLinkMagazine
{
    private readonly AutoLinkMagazineSettings settings;

    public GrabItAutoLinkMagazine(AutoLinkMagazineSettings settings)
    {
        this.settings = settings;
    }

    public int Number => this.settings.Number;

    public async Task SubscribeForStateChangedAsync(
        IStorageStateNotifier magazineStateNotifier,
        CancellationToken cancellationToken)
    {
        await magazineStateNotifier.NotifyAsync(State.Ready);
        cancellationToken.WaitHandle.WaitOne();
    }

    public async Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken)
    {
        await alarmsNotifier.NotifyAsync(ReadOnlyList.Empty<Alarm>());
        cancellationToken.WaitHandle.WaitOne();
    }
}