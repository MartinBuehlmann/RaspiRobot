namespace RaspiRobot.Web.Features.Devices.Robot.LiveUpdate;

using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using EventBroker;
using Microsoft.AspNetCore.SignalR;
using RaspiRobot.RobotControl.Devices.Robot.AxisPosition;
using RaspiRobot.Web.LiveUpdate;

public class RobotAxisPositionChangedObserver :
    IEventSubscriptionAsync<RobotAxisPositionChangedEvent>,
    ILiveUpdateEventObserver
{
    private readonly IHubContext<RobotAxisPositionChangedHub> robotAxisPositionChangedHub;
    private readonly Log log;
    private readonly SemaphoreSlim semaphore;
    private bool canTriggerEvent = true;
    private Timer? timer;

    public RobotAxisPositionChangedObserver(
        IHubContext<RobotAxisPositionChangedHub> robotAxisPositionChangedHub,
        Log log)
    {
        this.robotAxisPositionChangedHub = robotAxisPositionChangedHub;
        this.log = log;
        this.semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task HandleAsync(RobotAxisPositionChangedEvent data)
    {
        await this.semaphore.WaitAsync();
        if (this.canTriggerEvent)
        {
            this.canTriggerEvent = false;
            this.timer = new Timer(this.OnTimer, null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
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
            this.log.Debug("Notifying clients about robot axis position changed");
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