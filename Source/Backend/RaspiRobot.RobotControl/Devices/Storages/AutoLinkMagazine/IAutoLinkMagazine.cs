namespace RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;

public interface IAutoLinkMagazine : IDevice
{
    int Number { get; }

    Task SubscribeForStateChangedAsync(
        IStorageStateNotifier storageStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);
}