namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

public interface IMdiRobot
{
    bool Step(Axis axis, AxisDirection direction);
}