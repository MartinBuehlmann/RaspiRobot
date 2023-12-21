namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot.State;
using RobotState = RaspiRobot.RobotControl.Devices.Robot.State.RobotState;

internal class RobotStateNotifier : IRobotStateNotifier, IEventSubscriptionAsync<RobotStateChangedEvent>
{
    private readonly RobotStateConverter robotStateConverter;
    private readonly IServerStreamWriter<StateResponse> responseStream;
    private readonly IRobotStateRetriever robotStateRetriever;

    public RobotStateNotifier(
        RobotStateConverter robotStateConverter,
        IServerStreamWriter<StateResponse> responseStream,
        IRobotStateRetriever robotStateRetriever)
    {
        this.robotStateConverter = robotStateConverter;
        this.responseStream = responseStream;
        this.robotStateRetriever = robotStateRetriever;
    }

    public async Task NotifyAsync(RobotState state)
    {
        Erowa.OpenAPI.Robot.RobotState robotState = this.robotStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new StateResponse
            {
                State = robotState,
            });
    }

    public async Task HandleAsync(RobotStateChangedEvent _)
    {
        await this.NotifyAsync(this.robotStateRetriever.RobotState);
    }
}