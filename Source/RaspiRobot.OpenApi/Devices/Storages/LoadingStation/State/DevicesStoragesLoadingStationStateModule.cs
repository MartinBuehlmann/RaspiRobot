namespace RaspiRobot.OpenApi.Devices.Storages.LoadingStation.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;

internal class DevicesStoragesLoadingStationStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LoadingStationStateConverter>();
        builder.RegisterType<LoadingStationStateNotifier>().As<ILoadingStationStateNotifier>();
    }
}