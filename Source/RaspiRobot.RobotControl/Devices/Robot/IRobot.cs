namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;

public interface IRobot : IDevice
{
    Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForChuckLoadingsChangedAsync(
        IChuckLoadingsNotifier chuckLoadingsNotifier,
        CancellationToken cancellationToken);

    Task<ICommandResponse> LoadChuckAsync(StoragePlace sourcePlace, MachineChuck chuck, StoragePlace? destinationPlaceForPalletOnChuck);

    Task<ICommandResponse> UnloadChuckAsync(MachineChuck chuck, StoragePlace destinationPlace);

    Task<ICommandResponse> ExchangePlaceAsync(StoragePlace sourcePlace, StoragePlace destinationPlace);
}