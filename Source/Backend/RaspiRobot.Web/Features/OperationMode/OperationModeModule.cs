namespace RaspiRobot.Web.Features.OperationMode;

using Autofac;
using EventBroker.Autofac;
using RaspiRobot.Web.LiveUpdate;

internal class OperationModeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OperationModeChangedHub>();
        builder.RegisterType<OperationModeChangedObserver>()
            .As<ILiveUpdateEventObserver>()
            .RegisterOnEventBroker();
    }
}