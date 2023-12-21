namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using Autofac;

internal class RobotTransportSequenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TransportSequenceBuilder>();
        builder.RegisterType<TransportSequenceExecutor>();
        builder.RegisterType<TransportSequenceStepInterpolator>();
    }
}