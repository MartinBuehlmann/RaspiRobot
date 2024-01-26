namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

internal class DevicesStoragesStorageStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageStateConverter>();
        builder.RegisterType<StorageStateNotifier>().As<IMagazineStateNotifier>();
    }
}