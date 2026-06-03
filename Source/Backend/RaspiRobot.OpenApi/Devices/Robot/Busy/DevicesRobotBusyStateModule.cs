namespace RaspiRobot.OpenApi.Devices.Robot.Busy;

using Autofac;
using EventBroker.Autofac;

internal class DevicesRobotBusyStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotBusyStateConverter>();
        builder.RegisterType<RobotBusyStateNotifier>()
            .RegisterOnEventBroker();
    }
}