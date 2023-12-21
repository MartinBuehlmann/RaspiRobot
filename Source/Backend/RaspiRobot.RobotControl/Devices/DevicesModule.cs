namespace RaspiRobot.RobotControl.Devices;

using Autofac;
using Common;

internal class DevicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeviceCreator>();
        builder.RegisterType<DeviceRegistry>();

        builder.RegisterType<RobotControlDeviceService>()
            .As<IBackgroundService>()
            .As<IDeviceService>()
            .SingleInstance();
    }
}