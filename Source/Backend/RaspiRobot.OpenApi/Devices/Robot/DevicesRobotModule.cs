namespace RaspiRobot.OpenApi.Devices.Robot;

using Autofac;

internal class DevicesRobotModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StartLoadChuckRequestHandler>();
        builder.RegisterType<StartUnloadChuckRequestHandler>();
    }
}