namespace RaspiRobot.RobotControl;

using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;

public interface IDeviceService
{
    IRobot RetrieveRobot();

    IMagazine RetrieveMagazine(int number);
}