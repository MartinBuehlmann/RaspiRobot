namespace RaspiRobot.OpenApi.Devices.Robot.Busy;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Robot.Busy;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot;

internal class RobotBusyService : Erowa.OpenAPI.Robot.Busy.RobotBusyService.RobotBusyServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public RobotBusyService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveBusyStateChanged(
        Empty request,
        IServerStreamWriter<RetrieveBusyStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var robotStateNotifier = this.factory.Create<RobotBusyStateNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForStateChangedAsync(robotStateNotifier, cancellationTokenSource.Token);
    }
}