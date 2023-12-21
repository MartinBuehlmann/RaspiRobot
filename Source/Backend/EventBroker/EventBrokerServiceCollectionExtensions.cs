namespace EventBroker;

using Microsoft.Extensions.DependencyInjection;

public static class EventBrokerServiceCollectionExtensions
{
    public static IServiceCollection AddEventBroker(this IServiceCollection services)
    {
        services.AddSingleton<IEventBroker, EventBroker>();
        services.AddSingleton<IEventRegistration, EventRegistration>();
        services.AddTransient<EventSubscriber>();
        return services;
    }
}