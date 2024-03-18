namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class MoveStepSettings : IStepSettings
{
    public List<PositionSettings> Positions { get; } = new();
}