namespace RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

using System.Collections.Generic;

public record ChuckOccupancyChangedEvent(IReadOnlyList<ChuckOccupancy> ChuckOccupancies);