namespace RaspiRobot.Web.Features.OperationMode.LiveUpdate;

using Autofac;
using EventBroker.Autofac;
using RaspiRobot.Web.LiveUpdate;

internal class OperationModeLiveUpdateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OperationModeChangedHub>();
        builder.RegisterType<OperationModeChangedObserver>()
            .As<ILiveUpdateEventObserver>()
            .RegisterOnEventBroker();
    }
}