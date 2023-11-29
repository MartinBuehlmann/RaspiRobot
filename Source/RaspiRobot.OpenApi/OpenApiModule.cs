namespace RaspiRobot.OpenApi;

using Autofac;
using RaspiRobot.OpenApi.Devices.Magazine.State;
using RaspiRobot.OpenApi.Devices.Robot.State;
using RaspiRobot.OpenApi.Devices.Shared.Alarms;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;

public class OpenApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AlarmConverter>();
        builder.RegisterType<AlarmsNotifier>().As<IAlarmsNotifier>();

        builder.RegisterType<MagazineStateConverter>();
        builder.RegisterType<MagazineStateNotifier>().As<IMagazineStateNotifier>();

        builder.RegisterType<RobotStateConverter>();
        builder.RegisterType<RobotStateNotifier>().As<IRobotStateNotifier>();
    }
}