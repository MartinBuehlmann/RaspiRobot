namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages;

internal class DevicesStorageStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageStateConverter>();
        builder.RegisterType<StorageStateNotifier>().As<IStorageStateNotifier>();
    }
}