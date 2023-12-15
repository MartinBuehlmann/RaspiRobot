namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Mdi;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;

internal class DevicesRobotMdiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMdiRobot>().As<IMdiRobot>();
    }
}