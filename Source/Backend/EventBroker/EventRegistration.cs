namespace EventBroker;

using System;
using System.Collections.Generic;
using System.Linq;
using Common;

internal class EventRegistration : IEventRegistration
{
    private readonly object subscriptionLock;

    private readonly IDictionary<Type, List<IEventSubscriptionBase>> subscriptions =
        new Dictionary<Type, List<IEventSubscriptionBase>>();

    public EventRegistration()
    {
        this.subscriptionLock = new object();
    }

    public IReadOnlyList<IEventSubscriptionBase> Retrieve<TEventData>(TEventData data)
        where TEventData : class
    {
        lock (this.subscriptionLock)
        {
            Type eventDataType = data.GetType();
            if (this.subscriptions.TryGetValue(eventDataType, out var subscription))
            {
                return subscription.ToList();
            }
        }

        return ReadOnlyList.Empty<IEventSubscriptionBase>();
    }

    public void Register(IEventSubscriptionBase instance)
    {
        IReadOnlyList<Type> eventDataTypes = RetrieveEventDataTypes(instance);
        lock (this.subscriptionLock)
        {
            foreach (Type eventDataType in eventDataTypes)
            {
                if (!this.subscriptions.ContainsKey(eventDataType))
                {
                    this.subscriptions.Add(eventDataType, new List<IEventSubscriptionBase>());
                }

                this.subscriptions[eventDataType].Add(instance);
            }
        }
    }

    public void Unregister(IEventSubscriptionBase instance)
    {
        IReadOnlyList<Type> eventDataType = RetrieveEventDataTypes(instance);
        lock (this.subscriptionLock)
        {
            eventDataType.ForEach(x => this.subscriptions[x].Remove(instance));
        }
    }

    private static IReadOnlyList<Type> RetrieveEventDataTypes(object instance)
    {
        List<Type> types = instance.GetType().GetInterfaces()
            .Where(x => x is { IsGenericType: true, GenericTypeArguments.Length: 1 })
            .Where(x => x.GetGenericTypeDefinition() == typeof(IEventSubscription<>) ||
                        x.GetGenericTypeDefinition() == typeof(IEventSubscriptionAsync<>))
            .SelectMany(x => x.GetGenericArguments())
            .ToList();

        if (types.Any())
        {
            return types;
        }

        throw new NotSupportedException(
            "Registered class must implement IEventSubscription or IEventSubscriptionAsync");
    }
}