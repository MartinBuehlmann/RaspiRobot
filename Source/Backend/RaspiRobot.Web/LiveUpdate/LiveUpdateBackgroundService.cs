namespace RaspiRobot.Web.LiveUpdate;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.DependencyInjection;
using EventBroker;

internal class LiveUpdateBackgroundService : IBackgroundService
{
    private readonly IEventRegistration eventRegistration;
    private readonly Factory factory;
    private readonly List<ILiveUpdateEventObserver> liveUpdateEventObservers;

    public LiveUpdateBackgroundService(
        IEventRegistration eventRegistration,
        Factory factory)
    {
        this.eventRegistration = eventRegistration;
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
        foreach (IEventSubscriptionBase eventSubscription in
                 this.liveUpdateEventObservers.OfType<IEventSubscriptionBase>())
        {
            this.eventRegistration.Unregister(eventSubscription);
        }

        this.liveUpdateEventObservers.Clear();
        return Task.CompletedTask;
    }
}