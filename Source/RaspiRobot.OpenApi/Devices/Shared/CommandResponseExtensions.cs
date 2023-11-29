namespace RaspiRobot.OpenApi.Devices.Shared;

using Erowa.OpenAPI.Robot;
using RaspiRobot.RobotControl.Devices.Commands;

public static class CommandResponseExtensions
{
    public static CommandResponse ToCommandResponse(this ICommandResponse response)
    {
        return response is ErrorResponse errorResponse
            ? new CommandResponse
            {
                Error = new Error
                {
                    Message = errorResponse.Message,
                },
            }
            : new CommandResponse
            {
                Success = new Success(),
            };
    }
}