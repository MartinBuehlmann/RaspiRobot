namespace RaspiRobot.RobotControl.Devices.Robot.Steps;

using System.Collections.Generic;

public record Sequence(IReadOnlyList<IStep> Steps);