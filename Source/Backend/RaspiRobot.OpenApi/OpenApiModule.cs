namespace RaspiRobot.OpenApi;

using Autofac;
using RaspiRobot.OpenApi.Communication;
using RaspiRobot.OpenApi.Devices.Robot;
using RaspiRobot.OpenApi.Devices.Robot.Alarms;
using RaspiRobot.OpenApi.Devices.Robot.Busy;
using RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;
using RaspiRobot.OpenApi.Devices.Robot.Connection;
using RaspiRobot.OpenApi.Devices.Robot.State;
using RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;
using RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;
using RaspiRobot.OpenApi.Devices.Storages.Storage.State;

public class OpenApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<CommunicationModule>();
        builder.RegisterModule<DevicesRobotModule>();
        builder.RegisterModule<DevicesRobotAlarmsModule>();
        builder.RegisterModule<DevicesRobotBusyStateModule>();
        builder.RegisterModule<DevicesRobotChuckOccupancyModule>();
        builder.RegisterModule<DevicesRobotConnectionModule>();
        builder.RegisterModule<DevicesRobotStateModule>();
        builder.RegisterModule<DevicesStorageStateModule>();
        builder.RegisterModule<DevicesStorageAlarmsModule>();
        builder.RegisterModule<DevicesStorageConnectionModule>();
    }
}