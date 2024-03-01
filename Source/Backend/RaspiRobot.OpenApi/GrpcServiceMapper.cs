namespace RaspiRobot.OpenApi;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RaspiRobot.OpenApi.Devices.Robot;
using RaspiRobot.OpenApi.Devices.Storages.Storage;

public class GrpcServiceMapper
{
    public static void MapGrpcServices(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<StorageService>();
        endpoints.MapGrpcService<RobotService>();
    }
}