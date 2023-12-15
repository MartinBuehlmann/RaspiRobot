namespace RaspiRobot.RobotControl.GrabIt.Drivers;

using Autofac;

internal class DriverModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Pca9685Driver>();
    }
}