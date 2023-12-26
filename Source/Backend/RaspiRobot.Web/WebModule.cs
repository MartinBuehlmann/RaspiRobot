namespace RaspiRobot.Web;

using Autofac;
using RaspiRobot.Web.Features.OperationMode;
using RaspiRobot.Web.LiveUpdate;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<LiveUpdateModule>();
        builder.RegisterModule<OperationModeModule>();
    }
}