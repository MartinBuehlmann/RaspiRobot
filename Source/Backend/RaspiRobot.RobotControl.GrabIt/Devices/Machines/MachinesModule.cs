namespace RaspiRobot.RobotControl.GrabIt.Devices.Machines;

using Autofac;
using RaspiRobot.RobotControl.Devices.Machines;

internal class MachinesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMachine>().As<IMachine>();
    }
}