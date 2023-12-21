namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using Autofac;

internal class RobotSimulationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SimulationDriver>().As<IGrabItDriver>();
    }
}