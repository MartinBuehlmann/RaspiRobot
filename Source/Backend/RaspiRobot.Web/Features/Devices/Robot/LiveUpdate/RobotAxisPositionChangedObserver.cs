namespace RaspiRobot.Web.Features.Devices.Robot.LiveUpdate;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventBroker;
using Microsoft.AspNetCore.SignalR;
using RaspiRobot.RobotControl.Devices.Robot.AxisPosition;
using RaspiRobot.Web.LiveUpdate;

public class RobotAxisPositionChangedObserver :
    IEventSubscriptionAsync<RobotAxisPositionChangedEvent>,
    ILiveUpdateEventObserver
{
    private readonly IHubContext<RobotAxisPositionChangedHub> robotAxisPositionChangedHub;
    private readonly SemaphoreSlim semaphore;
    private bool canTriggerEvent = true;
    private Timer? timer;

    public RobotAxisPositionChangedObserver(
        IHubContext<RobotAxisPositionChangedHub> robotAxisPositionChangedHub)
    {
        this.robotAxisPositionChangedHub = robotAxisPositionChangedHub;
        this.semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task HandleAsync(RobotAxisPositionChangedEvent data)
    {
        await this.semaphore.WaitAsync();
        if (this.canTriggerEvent)
        {
            this.canTriggerEvent = false;
            this.timer = new Timer(this.OnTimer, null, TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
        }

        this.semaphore.Release();
    }

#pragma warning disable VSTHRD100
    private async void OnTimer(object? state)
#pragma warning restore VSTHRD100
    {
        await this.semaphore.WaitAsync();
        await this.timer!.DisposeAsync();

        try
        {
            await this.robotAxisPositionChangedHub.Clients.All
                .SendAsync(
                    "RobotAxisPositionChanged",
                    null);

            this.canTriggerEvent = true;
        }
        finally
        {
            this.semaphore.Release();
        }
    }
}