namespace RaspiRobot.OpenApi.Devices.Robot.Alarms;

using Autofac;

internal class DevicesRobotAlarmsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RobotAlarmConverter>();
        builder.RegisterType<RobotAlarmsNotifier>();
    }
}