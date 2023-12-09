namespace RaspiRobot.RobotControl.Devices.Storages.LoadingStation;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;

public interface ILoadingStation : IDevice
{
    int Number { get; }

    Task SubscribeForStateChangedAsync(
        IStorageStateNotifier storageStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);
}