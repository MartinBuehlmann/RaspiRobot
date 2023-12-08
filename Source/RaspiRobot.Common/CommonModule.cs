namespace RaspiRobot.Common;

using Autofac;
using RaspiRobot.Common.DependencyInjection;
using RaspiRobot.Common.Logging;

public class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Factory>();
        builder.RegisterType<Log>().SingleInstance();
    }
}