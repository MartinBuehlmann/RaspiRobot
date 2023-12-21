namespace RaspiRobot.OpenApi.Devices.Robot.ChuckLoading;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot;

internal class DevicesRobotChuckLoadingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ChuckLoadingConverter>();
        builder.RegisterType<ChuckLoadingsNotifier>().As<IChuckLoadingsNotifier>();
    }
}