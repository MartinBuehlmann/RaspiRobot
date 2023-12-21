namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotStateCache>()
            .As<RobotStateCache>()
            .As<IRobotStateRetriever>()
            .SingleInstance();
    }
}