namespace EventBroker;

using System.Threading.Tasks;

public interface IEventSubscriptionAsync<in T> : IEventSubscriptionBase
{
    Task HandleAsync(T data);
}