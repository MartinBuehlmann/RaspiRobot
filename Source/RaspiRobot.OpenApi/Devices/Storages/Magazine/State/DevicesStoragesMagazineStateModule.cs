namespace RaspiRobot.OpenApi.Devices.Storages.Magazine.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

internal class DevicesStoragesMagazineStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MagazineStateConverter>();
        builder.RegisterType<MagazineStateNotifier>().As<IMagazineStateNotifier>();
    }
}