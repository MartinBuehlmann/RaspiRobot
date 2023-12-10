namespace RaspiRobot.RobotControl.GrabIt;

using Autofac;
using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.GrabIt.Devices.Machines;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.GrabIt.Settings;

public class RobotControlGrabItModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrabItMachine>().As<IMachine>();
        builder.RegisterType<GrabItAutoLinkMagazine>().As<IAutoLinkMagazine>();
        builder.RegisterType<GrabItLoadingStation>().As<ILoadingStation>();
        builder.RegisterType<GrabItMagazine>().As<IMagazine>();
        builder.RegisterType<GrabItRobot>().As<IRobot>();
        builder.RegisterType<TransportSequenceBuilder>();
        builder.RegisterType<TransportSequenceExecutor>();

        builder.RegisterType<DefaultCellSettingsProvider>().As<IDefaultCellSettingsProvider>();
        builder.RegisterType<GrabItJsonConverterProvider>().As<IJsonConverterProvider>();

        builder.RegisterType<Pca9685Driver>();

        // Depending on if the software is running on the Raspberry PI or on the Computer,
        // on of the binding needs to be active.
        builder.RegisterType<GrabItDriver>().As<IGrabItDriver>();
        //builder.RegisterType<SimulationDriver>().As<IGrabItDriver>();
    }
}