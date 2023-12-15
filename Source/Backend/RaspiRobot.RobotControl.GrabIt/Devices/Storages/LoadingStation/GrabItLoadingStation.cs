namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.LoadingStation;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;

internal class GrabItLoadingStation : ILoadingStation
{
    private readonly LoadingStationSettings settings;

    public GrabItLoadingStation(LoadingStationSettings settings)
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