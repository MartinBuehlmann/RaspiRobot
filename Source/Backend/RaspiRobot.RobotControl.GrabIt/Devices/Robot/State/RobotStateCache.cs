namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.State;

using System;
using EventBroker;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotStateCache : IRobotStateRetriever, IDisposable
{
    private readonly IOperationModeRetriever operationModeRetriever;
    private readonly IEventBroker eventBroker;

    public RobotStateCache(
        IOperationModeRetriever operationModeRetriever,
        IEventBroker eventBroker)
    {
        this.operationModeRetriever = operationModeRetriever;
        this.eventBroker = eventBroker;

        this.operationModeRetriever.OperationModeChanged += this.HandleOperationModeChanged;
    }

    public event EventHandler? StateChangedChanged;

    public RobotState RobotState { get; private set; }

    public void Dispose()
    {
        this.operationModeRetriever.OperationModeChanged -= this.HandleOperationModeChanged;
    }

    private void HandleOperationModeChanged(object? sender, EventArgs e)
    {
        OperationMode operationMode = this.operationModeRetriever.OperationMode;
        switch (operationMode)
        {
            case OperationMode.Automatic:
                this.SetRobotState(RobotState.Ready);
                break;
            case OperationMode.Mdi:
            case OperationMode.NotReady:
                this.SetRobotState(RobotState.NotReady);
                break;
            default:
                throw new InvalidOperationException($"Illegal operation mode '{operationMode}'.");
        }
    }

    private void SetRobotState(RobotState robotState)
    {
        if (this.RobotState != robotState)
        {
            this.RobotState = robotState;
            this.StateChangedChanged?.Invoke(this, EventArgs.Empty);
            this.eventBroker.Publish(new RobotStateChangedEvent());
        }
    }
}