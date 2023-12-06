namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot;

public class RobotStateNotifier : IRobotStateNotifier
{
    private readonly RobotStateConverter robotStateConverter;
    private readonly IServerStreamWriter<StateResponse> responseStream;

    public RobotStateNotifier(
        RobotStateConverter robotStateConverter,
        IServerStreamWriter<StateResponse> responseStream)
    {
        this.robotStateConverter = robotStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        RobotState robotState = this.robotStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new StateResponse
            {
                State = robotState,
            });
    }
}