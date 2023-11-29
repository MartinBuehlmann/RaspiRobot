namespace RaspiRobot.RobotControl.GrabIt;

using Autofac;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Devices.Machines;
using RaspiRobot.RobotControl.GrabIt.Devices.Magazine;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Settings;

public class RobotControlGrabItModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMachine>().As<IMachine>();
        builder.RegisterType<GrabItMagazine>().As<IMagazine>();
        builder.RegisterType<GrabItRobot>().As<IRobot>();
        builder.RegisterType<DefaultCellSettingsProvider>().As<IDefaultCellSettingsProvider>();
    }
}