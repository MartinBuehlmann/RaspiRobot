namespace RaspiRobot.OpenApi.Devices.Shared;

using Erowa.OpenAPI.Robot;
using RaspiRobot.RobotControl.Devices.Commands;

internal static class CommandResponseExtensions
{
    public static CommandResponse ToCommandResponse(this ICommandResponse response)
    {
        return response is ErrorResponse errorResponse
            ? new CommandResponse
            {
                NotSuccessful = new NotSuccessful()
                {
                    Message = errorResponse.Message,
                },
            }
            : new CommandResponse
            {
                Successful = new Successful(),
            };
    }
}