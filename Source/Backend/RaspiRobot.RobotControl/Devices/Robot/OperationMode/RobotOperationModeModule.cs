namespace RaspiRobot.RobotControl.Devices.Robot.OperationMode;

using Autofac;

internal class RobotOperationModeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OperationModeCache>()
            .As<IOperationModeSetter>()
            .As<IOperationModeRetriever>()
            .SingleInstance();
    }
}