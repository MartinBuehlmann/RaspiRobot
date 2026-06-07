namespace RaspiRobot.RobotControl.Devices.Storages;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Connection;
using RaspiRobot.RobotControl.Devices.Storages.State;

public interface IStorage : IDevice
{
    string Identifier { get; }

    Task SubscribeForStateChangedAsync(
        IStorageStateNotifier storageStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForConnectionStateChangedAsync(
        IConnectionStateNotifier connectionStateNotifier,
        CancellationToken cancellationToken);
}