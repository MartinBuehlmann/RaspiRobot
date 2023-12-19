namespace EventBroker;

using System;
using Microsoft.Extensions.DependencyInjection;

public class EventSubscriber
{
    private readonly IServiceProvider serviceProvider;

    public EventSubscriber(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void Subscribe(IEventSubscriptionBase eventSubscription)
    {
        var subscription = this.serviceProvider.GetService<IEventRegistration>()!;
        subscription.Register(eventSubscription);
    }

    public void Unsubscribe(IEventSubscriptionBase eventSubscription)
    {
        var subscription = this.serviceProvider.GetService<IEventRegistration>()!;
        subscription.Unregister(eventSubscription);
    }
}