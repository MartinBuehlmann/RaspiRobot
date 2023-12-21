namespace RaspiRobot.RobotControl;

using Autofac;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;
using RaspiRobot.RobotControl.Settings;

public class RobotControlModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DevicesModule>();
        builder.RegisterModule<RobotOperationModeModule>();
        builder.RegisterModule<SettingsModule>();
    }
}