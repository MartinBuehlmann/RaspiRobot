namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.MachineLoading;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot;

internal class MachineLoadingService : Erowa.OpenAPI.MachineLoading.MachineLoadingService.MachineLoadingServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public MachineLoadingService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveMachineLoadingChanged(
        RetrieveMachineLoadingChangedRequest request,
        IServerStreamWriter<RetrieveMachineLoadingChangedResponse> responseStream,
        ServerCallContext context)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var chuckLoadingsNotifier = this.factory.Create<ChuckOccupancyNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        int[] chuckNumbers = request.Chucks.Select(x => int.Parse(x.Identifier, CultureInfo.InvariantCulture)).ToArray();
        await robot.SubscribeForChuckLoadingsChangedAsync(
            chuckNumbers,
            chuckLoadingsNotifier,
            cancellationTokenSource.Token);
    }
}