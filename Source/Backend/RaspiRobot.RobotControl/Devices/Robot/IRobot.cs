namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.State;
using RaspiRobot.RobotControl.Devices.Storages;

public interface IRobot : IDevice
{
    IMdiRobot MdiRobot { get; }

    Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForChuckLoadingsChangedAsync(
        int[] chuckNumbers,
        IChuckLoadingsNotifier chuckLoadingsNotifier,
        CancellationToken cancellationToken);

    Task<ICommandResponse> LoadChuckAsync(
        StoragePlace sourcePlace,
        MachineChuck chuck,
        StoragePlace? destinationPlaceForPalletOnChuck);

    Task<ICommandResponse> UnloadChuckAsync(
        MachineChuck chuck,
        StoragePlace destinationPlace);

    Task<ICommandResponse> ExchangePlaceAsync(
        StoragePlace sourcePlace,
        StoragePlace destinationPlace);
}