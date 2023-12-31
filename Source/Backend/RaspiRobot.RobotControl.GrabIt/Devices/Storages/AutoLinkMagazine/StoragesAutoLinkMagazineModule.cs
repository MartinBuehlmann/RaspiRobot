namespace RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;

internal class StoragesAutoLinkMagazineModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItAutoLinkMagazine>().As<IAutoLinkMagazine>();
    }
}