namespace RaspiRobot.Web.Features.OperationMode;

using System.Threading.Tasks;
using EventBroker;
using Microsoft.AspNetCore.SignalR;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;
using RaspiRobot.Web.LiveUpdate;

internal class OperationModeChangedObserver :
    IEventSubscriptionAsync<OperationModeChangedEvent>,
    ILiveUpdateEventObserver
{
    private readonly IOperationModeRetriever operationModeRetriever;
    private readonly IHubContext<OperationModeChangedHub> operationModeChangedHub;

    public OperationModeChangedObserver(
        IOperationModeRetriever operationModeRetriever,
        IHubContext<OperationModeChangedHub> operationModeChangedHub)
    {
        this.operationModeRetriever = operationModeRetriever;
        this.operationModeChangedHub = operationModeChangedHub;
    }

    public async Task HandleAsync(OperationModeChangedEvent data)
    {
        await this.operationModeChangedHub.Clients.All
            .SendAsync(
                "UpdateOperationMode",
                this.operationModeRetriever.OperationMode);
    }
}