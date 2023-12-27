namespace RaspiRobot.Web.Features.Devices.Robot.LiveUpdate;

using Autofac;
using EventBroker.Autofac;
using RaspiRobot.Web.LiveUpdate;

internal class RobotLiveUpdateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotAxisPositionChangedHub>();
        builder.RegisterType<RobotAxisPositionChangedObserver>()
            .As<ILiveUpdateEventObserver>()
            .RegisterOnEventBroker();
    }

}