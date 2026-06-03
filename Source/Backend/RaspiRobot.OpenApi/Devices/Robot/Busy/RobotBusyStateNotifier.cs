namespace RaspiRobot.OpenApi.Devices.Robot.Busy;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot.Busy;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotBusyStateNotifier : IRobotStateNotifier, IEventSubscriptionAsync<RobotStateChangedEvent>
{
    private readonly RobotBusyStateConverter robotBusyStateConverter;
    private readonly IServerStreamWriter<RetrieveBusyStateChangedResponse> responseStream;
    private readonly IRobotStateRetriever robotStateRetriever;

    public RobotBusyStateNotifier(
        RobotBusyStateConverter robotBusyStateConverter,
        IServerStreamWriter<RetrieveBusyStateChangedResponse> responseStream,
        IRobotStateRetriever robotStateRetriever)
    {
        this.robotBusyStateConverter = robotBusyStateConverter;
        this.responseStream = responseStream;
        this.robotStateRetriever = robotStateRetriever;
    }

    public async Task NotifyAsync(RobotState state)
    {
        await this.responseStream.WriteAsync(
            this.robotBusyStateConverter.Convert(state));
    }

    public async Task HandleAsync(RobotStateChangedEvent state)
    {
        await this.NotifyAsync(this.robotStateRetriever.RobotState);
    }
}