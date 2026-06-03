namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;

using Autofac;

internal class DevicesStorageConnectionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageConnectionStateNotifier>();
    }
}