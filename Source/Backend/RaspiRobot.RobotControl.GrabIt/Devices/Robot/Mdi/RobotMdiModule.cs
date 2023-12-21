namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Mdi;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;

internal class RobotMdiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMdiRobot>().As<IMdiRobot>();
    }
}