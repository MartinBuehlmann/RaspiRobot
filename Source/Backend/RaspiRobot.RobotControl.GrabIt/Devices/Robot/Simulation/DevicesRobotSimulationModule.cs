namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using Autofac;

internal class DevicesRobotSimulationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SimulationDriver>().As<IGrabItDriver>();
    }
}