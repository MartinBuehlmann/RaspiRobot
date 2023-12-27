namespace RaspiRobot.Web.LiveUpdate;

using Autofac;
using Common;

internal class LiveUpdateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LiveUpdateBackgroundService>()
            .As<IBackgroundService>();
    }
}