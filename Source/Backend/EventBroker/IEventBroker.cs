namespace EventBroker;

public interface IEventBroker
{
    int QueuedEvents { get; }

    void Publish<T>(T data)
        where T : class;
}