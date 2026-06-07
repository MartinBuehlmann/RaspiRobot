namespace RaspiRobot.OpenApi.Devices.Robot.Connection;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Robot.Connection;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot;

internal class RobotConnectionService : Erowa.OpenAPI.Robot.Connection.RobotConnectionService.RobotConnectionServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public RobotConnectionService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveConnectionStateChanged(
        Empty request,
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var connectionStateNotifier = this.factory.Create<RobotConnectionStateNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForConnectionStateChangedAsync(connectionStateNotifier, cancellationTokenSource.Token);
    }
}