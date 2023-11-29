namespace RaspiRobot.Common;

using Autofac;
using RaspiRobot.Common.DependencyInjection;

public class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Factory>();
    }
}