namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class SequenceSettings
{
    public List<IStepSettings> Steps { get; } = new();
}