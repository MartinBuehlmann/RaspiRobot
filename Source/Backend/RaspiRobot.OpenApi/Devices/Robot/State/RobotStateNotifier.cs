namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System;
using System.Threading.Tasks;
using Erowa.OpenAPI.Robot;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;

internal class RobotStateNotifier : IRobotStateNotifier, IEventSubscriptionAsync<OperationModeChangedEvent>
{
    private readonly RobotStateConverter robotStateConverter;
    private readonly IServerStreamWriter<StateResponse> responseStream;
    private readonly IOperationModeRetriever operationModeRetriever;

    public RobotStateNotifier(
        RobotStateConverter robotStateConverter,
        IServerStreamWriter<StateResponse> responseStream,
        IOperationModeRetriever operationModeRetriever)
    {
        this.robotStateConverter = robotStateConverter;
        this.responseStream = responseStream;
        this.operationModeRetriever = operationModeRetriever;
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

    // TODO: This must be moved to the GrabItRobot, since this class here must not care about state logic.
    public async Task HandleAsync(OperationModeChangedEvent data)
    {
        OperationMode currentOperationMode = this.operationModeRetriever.OperationMode;

        State state = currentOperationMode switch
        {
            OperationMode.Automatic => State.Ready,
            OperationMode.Mdi => State.NotReady,
            OperationMode.NotReady => State.NotReady,
            _ => throw new ArgumentOutOfRangeException($"Unsupported operation mode '{currentOperationMode}'"),
        };

        await this.NotifyAsync(state);
    }
}