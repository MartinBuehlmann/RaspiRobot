namespace RaspiRobot.Web;

using Autofac;
using RaspiRobot.Web.Features.Devices.Robot.LiveUpdate;
using RaspiRobot.Web.Features.OperationMode.LiveUpdate;
using RaspiRobot.Web.LiveUpdate;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<LiveUpdateModule>();
        builder.RegisterModule<OperationModeLiveUpdateModule>();
        builder.RegisterModule<RobotLiveUpdateModule>();
    }
}