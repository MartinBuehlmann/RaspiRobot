namespace RaspiRobot.RobotControl;

using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

public interface IDeviceService
{
    IRobot RetrieveRobot();

    IAutoLinkMagazine RetrieveAutoLinkMagazine(int number);

    ILoadingStation RetrieveLoadingStation(int number);

    IMagazine RetrieveMagazine(int number);
}