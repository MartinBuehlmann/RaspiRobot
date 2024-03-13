namespace RaspiRobot.RobotControl.GrabIt;

using Autofac;
using RaspiRobot.RobotControl.GrabIt.Devices.Machines;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.OperationMode;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.State;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.GrabIt.Driver;
using RaspiRobot.RobotControl.GrabIt.Settings;

public class RobotControlGrabItModule : Module
{
    // Depending on if the software is running on the Raspberry PI or on the Computer,
    // DevicesRobotNativeModule or DevicesRobotSimulationModule needs to be loaded.
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DriverModule>();
        builder.RegisterModule<MachinesModule>();
        builder.RegisterModule<RobotMdiModule>();
        builder.RegisterModule<RobotModule>();
        //builder.RegisterModule<RobotNativeModule>();
        builder.RegisterModule<RobotOperationModeModule>();
        builder.RegisterModule<RobotSimulationModule>();
        builder.RegisterModule<RobotStateModule>();
        builder.RegisterModule<RobotTransportSequenceModule>();
        builder.RegisterModule<SettingsModule>();
        builder.RegisterModule<StoragesAutoLinkMagazineModule>();
        builder.RegisterModule<StoragesLoadingStationModule>();
        builder.RegisterModule<StoragesMagazineModule>();
    }
}