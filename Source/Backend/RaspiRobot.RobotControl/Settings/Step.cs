namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class Step
{
    public List<IPosition> Positions { get; } = new();
}