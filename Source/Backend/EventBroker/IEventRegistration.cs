namespace EventBroker;

public interface IEventRegistration
{
    void Register(IEventSubscriptionBase instance);

    void Unregister(IEventSubscriptionBase instance);
}