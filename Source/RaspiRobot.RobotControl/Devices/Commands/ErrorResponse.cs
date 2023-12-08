namespace RaspiRobot.RobotControl.Devices.Commands;

public record ErrorResponse(string Message) : ICommandResponse;