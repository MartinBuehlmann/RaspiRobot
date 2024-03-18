namespace RaspiRobot.RobotControl.Devices.Robot.Steps;

using System.Collections.Generic;

public record MoveStep(IReadOnlyList<Position> Positions) : IStep;