namespace RaspiRobot.Web.Features.Devices;

using Autofac;

internal class DevicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DevicesRetriever>();
    }
}