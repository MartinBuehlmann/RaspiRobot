namespace RaspiRobot.OpenApi;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RaspiRobot.OpenApi.Devices.Robot;
using RaspiRobot.OpenApi.Devices.Robot.Alarms;
using RaspiRobot.OpenApi.Devices.Robot.Busy;
using RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;
using RaspiRobot.OpenApi.Devices.Robot.Connection;
using RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;
using RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;
using RaspiRobot.OpenApi.Devices.Storages.Storage.Visulink;

public class GrpcServiceMapper
{
    public static void MapGrpcServices(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<VisuLinkService>();
        endpoints.MapGrpcService<StorageAlarmService>();
        endpoints.MapGrpcService<StorageConnectionService>();
        endpoints.MapGrpcService<RobotService>();
        endpoints.MapGrpcService<RobotAlarmService>();
        endpoints.MapGrpcService<RobotBusyService>();
        endpoints.MapGrpcService<RobotConnectionService>();
        endpoints.MapGrpcService<MachineLoadingService>();
    }
}