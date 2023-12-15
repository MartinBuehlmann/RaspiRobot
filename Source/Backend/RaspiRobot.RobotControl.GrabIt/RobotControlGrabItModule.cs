namespace RaspiRobot.RobotControl.GrabIt;

using Autofac;
using RaspiRobot.RobotControl.GrabIt.Devices.Machines;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;
using RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.GrabIt.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.GrabIt.Drivers;
using RaspiRobot.RobotControl.GrabIt.OperationMode;
using RaspiRobot.RobotControl.GrabIt.Settings;

public class RobotControlGrabItModule : Module
{
    // Depending on if the software is running on the Raspberry PI or on the Computer,
    // DevicesRobotNativeModule or DevicesRobotSimulationModule needs to be loaded.
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DevicesMachinesModule>();
        builder.RegisterModule<DevicesRobotModule>();
        builder.RegisterModule<DevicesRobotNativeModule>();
        //builder.RegisterModule<DevicesRobotSimulationModule>();
        builder.RegisterModule<DevicesRobotTransportSequenceModule>();
        builder.RegisterModule<DevicesStoragesAutoLinkMagazineModule>();
        builder.RegisterModule<DevicesStoragesLoadingStationModule>();
        builder.RegisterModule<DevicesStoragesMagazineModule>();
        builder.RegisterModule<DriverModule>();
        builder.RegisterModule<OperationModeModule>();
        builder.RegisterModule<SettingsModule>();
    }
}