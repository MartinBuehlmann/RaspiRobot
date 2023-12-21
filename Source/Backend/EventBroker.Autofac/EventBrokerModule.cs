using Autofac;

namespace EventBroker.Autofac;

public class EventBrokerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EventBroker>()
            .As<IEventBroker>()
            .SingleInstance();
        builder.RegisterType<EventRegistration>()
            .As<IEventRegistration>()
            .SingleInstance();
        builder.RegisterType<EventSubscriber>();
    }
}