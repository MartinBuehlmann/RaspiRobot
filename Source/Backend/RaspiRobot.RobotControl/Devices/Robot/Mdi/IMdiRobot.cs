namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

using RaspiRobot.RobotControl.Settings;

public interface IMdiRobot
{
    Position? Step(Axis axis, AxisDirection direction);

    int RetrievePosition(int axis);
}