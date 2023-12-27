namespace RaspiRobot.Web.LiveUpdate;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.DependencyInjection;

internal class LiveUpdateBackgroundService : IBackgroundService
{
    private readonly Factory factory;
    private readonly List<ILiveUpdateEventObserver> liveUpdateEventObservers;

    public LiveUpdateBackgroundService(Factory factory)
    {
        this.factory = factory;
        this.liveUpdateEventObservers = new List<ILiveUpdateEventObserver>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var updateEventObservers = this.factory.Create<IEnumerable<ILiveUpdateEventObserver>>();
        this.liveUpdateEventObservers.AddRange(updateEventObservers);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.liveUpdateEventObservers.Clear();
        return Task.CompletedTask;
    }
}