namespace RaspiRobot.RobotControl.GrabIt.Settings;

using Autofac;
using RaspiRobot.RobotControl.Devices;

internal class SettingsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DefaultCellSettingsProvider>().As<IDefaultCellSettingsProvider>();
        builder.RegisterType<GrabItJsonConverterProvider>().As<IJsonConverterProvider>();
    }
}