namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using Autofac;
using EventBroker.Autofac;

internal class DevicesRobotChuckOccupancyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ChuckOccupancyConverter>();
        builder.RegisterType<ChuckOccupancyNotifier>()
            .RegisterOnEventBroker();
    }
}