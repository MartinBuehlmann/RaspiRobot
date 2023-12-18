namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

using RaspiRobot.RobotControl.Settings;

public interface IMdiRobot
{
    IPosition? Step(Axis axis, AxisDirection direction);
}