namespace RaspiRobot.RobotControl.Settings;

using Autofac;

internal class SettingsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SettingsRetriever>().As<ISettingsRetriever>();
        builder.RegisterType<CellSettingsLoader>();
    }
}