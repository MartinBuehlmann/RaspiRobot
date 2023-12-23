namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

public interface IMdiRobot
{
    Position? Step(Axis axis, AxisDirection direction);

    IReadOnlyList<Position> RetrieveAxisPositions();
}