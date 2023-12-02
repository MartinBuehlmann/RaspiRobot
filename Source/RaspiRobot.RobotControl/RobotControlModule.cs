namespace RaspiRobot.RobotControl;

using Autofac;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices;

public class RobotControlModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotControlDeviceService>()
            .As<IBackgroundService>()
            .As<IDeviceService>()
            .SingleInstance();
        builder.RegisterType<SettingsRetriever>().As<ISettingsRetriever>();
        builder.RegisterType<CellSettingsLoader>();
        builder.RegisterType<DeviceCreator>();
        builder.RegisterType<DeviceRegistry>();
    }
}