namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

using RaspiRobot.RobotControl.Devices.Robot.Steps;

public interface IMdiRobot
{
    Position? Step(Axis axis, AxisDirection direction, int stepSize);
}