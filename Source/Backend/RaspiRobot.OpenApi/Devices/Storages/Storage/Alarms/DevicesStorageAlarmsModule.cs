namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;

using Autofac;

internal class DevicesStorageAlarmsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageAlarmConverter>();
        builder.RegisterType<StorageAlarmsNotifier>();
    }
}