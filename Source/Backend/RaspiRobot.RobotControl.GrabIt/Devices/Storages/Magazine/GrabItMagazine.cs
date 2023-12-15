namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;

internal class GrabItMagazine : IMagazine
{
    private readonly MagazineSettings settings;

    public GrabItMagazine(MagazineSettings settings)
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