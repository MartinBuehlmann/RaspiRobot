namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using Autofac;

internal class RobotNativeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItDriver>().As<IGrabItDriver>();
    }
}