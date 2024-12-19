namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using Autofac;
using EventBroker.Autofac;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

internal class DevicesRobotChuckOccupancyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ChuckOccupancyConverter>();
        builder.RegisterType<ChuckOccupancyNotifier>()
            .As<IChuckOccupancyNotifier>()
            .RegisterOnEventBroker();
    }
}