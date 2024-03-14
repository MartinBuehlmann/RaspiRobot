namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;
using Newtonsoft.Json;

public class SequenceSettings
{
    public List<IStepSettings> Steps { get; } = new();
}