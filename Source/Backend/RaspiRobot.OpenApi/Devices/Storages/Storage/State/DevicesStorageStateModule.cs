namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using Autofac;

internal class DevicesStorageStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageStateConverter>();
        builder.RegisterType<StorageStateNotifier>();
    }
}