namespace RaspiRobot.Web.Features.MDI.Robot;

using RaspiRobot.RobotControl.Settings;

public record StepResponseInfo(bool Executed, Position? NewPosition);