namespace RaspiRobot.RobotControl.GrabIt.Driver;

using Autofac;

internal class DriverModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Pca9685Driver>();
    }
}