namespace RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine.State;

using Autofac;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;

internal class DevicesStoragesAutoLinkMagazineStateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AutoLinkMagazineStateConverter>();
        builder.RegisterType<AutoLinkMagazineStateNotifier>().As<IAutoLinkMagazineStateNotifier>();
    }
}