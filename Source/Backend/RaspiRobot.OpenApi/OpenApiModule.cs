namespace RaspiRobot.OpenApi;

using Autofac;
using RaspiRobot.OpenApi.Communication;
using RaspiRobot.OpenApi.Devices.Robot;
using RaspiRobot.OpenApi.Devices.Robot.ChuckLoading;
using RaspiRobot.OpenApi.Devices.Robot.State;
using RaspiRobot.OpenApi.Devices.Shared.Alarms;
using RaspiRobot.OpenApi.Devices.Storages.Storage.State;

public class OpenApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<CommunicationModule>();
        builder.RegisterModule<DevicesRobotModule>();
        builder.RegisterModule<DevicesRobotChuckLoadingModule>();
        builder.RegisterModule<DevicesRobotStateModule>();
        builder.RegisterModule<DevicesSharedModule>();
        builder.RegisterModule<DevicesStoragesStorageStateModule>();
    }
}