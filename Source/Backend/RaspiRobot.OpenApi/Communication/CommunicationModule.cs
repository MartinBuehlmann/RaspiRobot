namespace RaspiRobot.OpenApi.Communication;

using Autofac;

internal class CommunicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrpcStreamListener>();
    }
}