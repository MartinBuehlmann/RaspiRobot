namespace RaspiRobot.OpenApi.Devices.Robot;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Robot;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.Common.DependencyInjection;
using RaspiRobot.OpenApi.Devices.Shared;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages;

public class RobotService : Robot.RobotBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public RobotService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveStateAndStateChanged(
        Empty request,
        IServerStreamWriter<StateResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var magazineStateNotifier = this.factory.Create<IRobotStateNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForStateChangedAsync(magazineStateNotifier, cancellationTokenSource.Token);
    }

    public override async Task RetrieveAlarmsAndAlarmsChanged(
        Empty request,
        IServerStreamWriter<AlarmResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var alarmsNotifier = this.factory.Create<IAlarmsNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForAlarmsChangedAsync(alarmsNotifier, cancellationTokenSource.Token);
    }

    public override async Task RetrieveChuckLoadingsAndChuckLoadingsChanged(
        ChuckLoadingsRequest request,
        IServerStreamWriter<ChuckLoadingsResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var chuckLoadingsNotifier = this.factory.Create<IChuckLoadingsNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        int[] chuckNumbers = request.Chucks.Select(x => x.Number).ToArray();
        await robot.SubscribeForChuckLoadingsChangedAsync(chuckNumbers, chuckLoadingsNotifier, cancellationTokenSource.Token);
    }

    public override async Task<CommandResponse> LoadChuck(LoadChuckRequest request, ServerCallContext context)
    {
        IRobot robot = this.deviceService.RetrieveRobot();

        StoragePlace? destinationPlaceForPalletOnChuck = request.PlaceToUnloadPalletOnChuck is not null
            ? new StoragePlace(request.PlaceToUnloadPalletOnChuck.Number)
            : null;
        ICommandResponse response = await robot.LoadChuckAsync(
            new StoragePlace(request.PlaceToLoad.Number),
            new MachineChuck(request.Chuck.Number),
            destinationPlaceForPalletOnChuck);

        return response.ToCommandResponse();
    }

    public override async Task<CommandResponse> UnloadChuck(UnloadChuckRequest request, ServerCallContext context)
    {
        IRobot robot = this.deviceService.RetrieveRobot();

        ICommandResponse response = await robot.UnloadChuckAsync(
            new MachineChuck(request.Chuck.Number),
            new StoragePlace(request.PlaceToUnload.Number));

        return response.ToCommandResponse();
    }

    public override async Task<CommandResponse> ExchangePlace(ExchangePlaceRequest request, ServerCallContext context)
    {
        IRobot robot = this.deviceService.RetrieveRobot();

        ICommandResponse response = await robot.ExchangePlaceAsync(
            new StoragePlace(request.SourcePlace.Number),
            new StoragePlace(request.DestinationPlace.Number));

        return response.ToCommandResponse();
    }
}