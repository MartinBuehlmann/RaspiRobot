namespace RaspiRobot.OpenApi.Devices.Robot.State;

using Autofac;
using EventBroker.Autofac;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class DevicesRobotStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotStateConverter>();
        builder.RegisterType<RobotStateNotifier>()
            .As<IRobotStateNotifier>()
            .RegisterOnEventBroker();
    }
}