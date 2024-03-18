namespace RaspiRobot.RobotControl.Devices.Storages;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;

public interface IStorage : IDevice
{
    int Number { get; }

    Task SubscribeForStateChangedAsync(
        IStorageStateNotifier storageStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);
}