namespace RaspiRobot.RobotControl.GrabIt.OperationMode;

using Autofac;
using RaspiRobot.RobotControl.OperationMode;

internal class OperationModeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItOperationModeByHardwareRetriever>().As<IOperationModeByHardwareRetriever>();
    }
}