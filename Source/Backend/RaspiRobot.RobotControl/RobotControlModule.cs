namespace RaspiRobot.RobotControl;

using Autofac;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class RobotControlModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DevicesModule>();
        builder.RegisterModule<SettingsModule>();
    }
}