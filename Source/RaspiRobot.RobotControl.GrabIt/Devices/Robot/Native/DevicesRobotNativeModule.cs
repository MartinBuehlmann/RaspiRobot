namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using Autofac;

internal class DevicesRobotNativeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItDriver>().As<IGrabItDriver>();
        builder.RegisterType<Pca9685Driver>();
    }
}