namespace RaspiRobot.OpenApi.Devices.Shared.Alarms;

using Autofac;
using RaspiRobot.RobotControl.Devices.Alarms;

internal class DevicesSharedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AlarmConverter>();
        builder.RegisterType<AlarmsNotifier>().As<IAlarmsNotifier>();
    }
}