namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.OperationMode;

using Autofac;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;

internal class RobotOperationModeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItOperationModeByHardwareRetriever>().As<IOperationModeByHardwareRetriever>();
    }
}