namespace RaspiRobot.RobotControl.OperationMode;

using System;
using EventBroker;

internal class OperationModeCache : IOperationModeRetriever, IOperationModeSetter
{
    private readonly IEventBroker eventBroker;

    public OperationModeCache(IEventBroker eventBroker)
    {
        this.eventBroker = eventBroker;
        // TODO: With a tri-state hardware switch, it should be by default off,
        //       respectively set by the hardware. With software only, we set it
        //       to Automatic and let it override by the web interface for Mdi mode.
        //       Hardware is currently not supported (hardware missing :-/).
        this.OperationMode = OperationMode.Automatic;
    }

    public event EventHandler? OperationModeChanged;

    public OperationMode OperationMode { get; private set; }

    public void SetOperationMode(OperationMode operationMode)
    {
        if (this.OperationMode != operationMode)
        {
            this.OperationMode = operationMode;
            this.OperationModeChanged?.Invoke(this, EventArgs.Empty);
            this.eventBroker.Publish(new OperationModeChangedEvent());
        }
    }
}