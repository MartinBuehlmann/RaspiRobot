namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.LoadingStation;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;

internal class StoragesLoadingStationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItLoadingStation>().As<ILoadingStation>();
    }
}