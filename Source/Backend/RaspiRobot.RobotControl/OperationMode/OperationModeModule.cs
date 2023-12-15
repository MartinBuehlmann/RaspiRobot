namespace RaspiRobot.RobotControl.OperationMode;

using Autofac;

internal class OperationModeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OperationModeCache>()
            .As<IOperationModeSetter>()
            .As<IOperationModeRetriever>()
            .SingleInstance();
    }
}