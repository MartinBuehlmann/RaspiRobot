namespace RaspiRobot.RobotControl.GrabIt.Devices.Alarms;

using Autofac;
using RaspiRobot.RobotControl.Devices.Alarms;

public class AlarmsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AlarmsFacade>().As<IAlarmsFacade>();
    }
}