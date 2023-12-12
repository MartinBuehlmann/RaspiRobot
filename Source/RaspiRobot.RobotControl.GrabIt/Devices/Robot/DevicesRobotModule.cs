namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot;

internal class DevicesRobotModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItRobot>().As<IRobot>();
    }
}