namespace RaspiRobot.RobotControl.Devices;

using RaspiRobot.RobotControl.Settings;

public interface IDefaultCellSettingsProvider
{
    CellSettings DefaultCellSettings { get; }
}