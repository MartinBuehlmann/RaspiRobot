namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

internal class StoragesMagazineModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMagazine>().As<IMagazine>();
    }
}