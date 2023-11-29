namespace RaspiRobot.RobotControl.Devices.Magazine;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;

public interface IMagazine : IDevice
{
    int Number { get; }

    Task SubscribeForStateChangedAsync(
        IMagazineStateNotifier magazineStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);
}