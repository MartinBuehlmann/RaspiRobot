namespace RaspiRobot.OpenApi.Devices.Robot.Connection;

using Autofac;

internal class DevicesRobotConnectionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotConnectionStateNotifier>();
    }
}