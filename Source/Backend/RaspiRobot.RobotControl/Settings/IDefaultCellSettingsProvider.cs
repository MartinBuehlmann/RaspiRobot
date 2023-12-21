namespace RaspiRobot.RobotControl.Settings;

public interface IDefaultCellSettingsProvider
{
    CellSettings DefaultCellSettings { get; }
}