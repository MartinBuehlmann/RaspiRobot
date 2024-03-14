namespace RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;

using System.Collections.Generic;

public record ChuckLoadingChangedEvent(IReadOnlyList<ChuckLoading> ChuckLoadings);