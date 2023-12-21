namespace Common;

using Autofac;
using Common.DependencyInjection;
using Common.Logging;

public class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationCrasher>();
        builder.RegisterType<Factory>();
        builder.RegisterType<Log>().SingleInstance();
    }
}