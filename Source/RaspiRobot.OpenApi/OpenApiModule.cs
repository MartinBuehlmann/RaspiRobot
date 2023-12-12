namespace RaspiRobot.OpenApi;

using Autofac;
using RaspiRobot.OpenApi.Devices.Robot.ChuckLoading;
using RaspiRobot.OpenApi.Devices.Robot.State;
using RaspiRobot.OpenApi.Devices.Shared.Alarms;
using RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine.State;
using RaspiRobot.OpenApi.Devices.Storages.LoadingStation.State;
using RaspiRobot.OpenApi.Devices.Storages.Magazine.State;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

public class OpenApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AlarmConverter>();
        builder.RegisterType<AlarmsNotifier>().As<IAlarmsNotifier>();

        builder.RegisterType<AutoLinkMagazineStateConverter>();
        builder.RegisterType<AutoLinkMagazineStateNotifier>().As<IAutoLinkMagazineStateNotifier>();
        builder.RegisterType<LoadingStationStateConverter>();
        builder.RegisterType<LoadingStationStateNotifier>().As<ILoadingStationStateNotifier>();
        builder.RegisterType<MagazineStateConverter>();
        builder.RegisterType<MagazineStateNotifier>().As<IMagazineStateNotifier>();

        builder.RegisterType<RobotStateConverter>();
        builder.RegisterType<RobotStateNotifier>().As<IRobotStateNotifier>();

        builder.RegisterType<ChuckLoadingConverter>();
        builder.RegisterType<ChuckLoadingsNotifier>().As<IChuckLoadingsNotifier>();
    }
}