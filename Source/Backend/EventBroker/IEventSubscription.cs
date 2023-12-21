namespace EventBroker;

public interface IEventSubscription<in T> : IEventSubscriptionBase
{
    void Handle(T data);
}