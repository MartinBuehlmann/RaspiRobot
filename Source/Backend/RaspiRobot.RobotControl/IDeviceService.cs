namespace RaspiRobot.RobotControl;

using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages;

public interface IDeviceService
{
    IRobot RetrieveRobot();

    TStorage RetrieveStorage<TStorage>(int number)
        where TStorage : IStorage;
}