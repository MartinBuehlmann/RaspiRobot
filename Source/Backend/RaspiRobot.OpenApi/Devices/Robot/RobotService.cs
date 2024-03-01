namespace RaspiRobot.OpenApi.Devices.Robot;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Robot;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.OpenApi.Communication;
using RaspiRobot.OpenApi.Devices.Shared;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;
using RaspiRobot.RobotControl.Devices.Robot.State;
using RaspiRobot.RobotControl.Devices.Storages;

internal class RobotService : Robot.RobotBase
{
    private readonly IDeviceService deviceService;
    private readonly StartLoadChuckRequestHandler startLoadChuckRequestHandler;
    private readonly StartUnloadChuckRequestHandler startUnloadChuckRequestHandler;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly GrpcStreamListener grpcStreamListener;
    private readonly Factory factory;

    public RobotService(
        IDeviceService deviceService,
        StartLoadChuckRequestHandler startLoadChuckRequestHandler,
        StartUnloadChuckRequestHandler startUnloadChuckRequestHandler,
        IHostApplicationLifetime hostApplicationLifetime,
        GrpcStreamListener grpcStreamListener,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.startLoadChuckRequestHandler = startLoadChuckRequestHandler;
        this.startUnloadChuckRequestHandler = startUnloadChuckRequestHandler;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.grpcStreamListener = grpcStreamListener;
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
        await robot.SubscribeForChuckLoadingsChangedAsync(
            chuckNumbers,
            chuckLoadingsNotifier,
            cancellationTokenSource.Token);
    }

    public override async Task<CommandResponse> LoadChuck(
        IAsyncStreamReader<LoadChuckRequest> requestStream,
        ServerCallContext context)
    {
        CommandResponse[] results = await this.grpcStreamListener.ListenAsync(
            requestStream,
            async request => await HandleLoadChuckRequestAsync(request),
            context.Peer,
            context.Method,
            context.CancellationToken);

        return results.SingleOrDefault() ??
               new CommandResponse
               {
                   NotSuccessful = new NotSuccessful
                   {
                       Message = "Command was not started",
                   },
               };

        async Task<CommandResponse> HandleLoadChuckRequestAsync(LoadChuckRequest loadChuckRequest)
        {
            if (loadChuckRequest.RequestCase is not LoadChuckRequest.RequestOneofCase.Start
                || loadChuckRequest.Start is not { } request)
            {
                throw new NotSupportedException($"{nameof(this.LoadChuck)} cancellation is not yet supported.");
            }

            return await this.startLoadChuckRequestHandler.HandleLoadChuckAsync(request);
        }
    }

    public override async Task<CommandResponse> UnloadChuck(
        IAsyncStreamReader<UnloadChuckRequest> requestStream,
        ServerCallContext context)
    {
        CommandResponse[] results = await this.grpcStreamListener.ListenAsync(
            requestStream,
            async request => await HandleUnloadChuckRequestAsync(request),
            context.Peer,
            context.Method,
            context.CancellationToken);

        return results.SingleOrDefault() ??
               new CommandResponse
               {
                   NotSuccessful = new NotSuccessful
                   {
                       Message = "Command was not started",
                   },
               };

        async Task<CommandResponse> HandleUnloadChuckRequestAsync(UnloadChuckRequest unloadChuckRequest)
        {
            if (unloadChuckRequest.RequestCase is not UnloadChuckRequest.RequestOneofCase.Start
                || unloadChuckRequest.Start is not { } request)
            {
                throw new NotSupportedException($"{nameof(this.UnloadChuck)} cancellation is not yet supported.");
            }

            return await this.startUnloadChuckRequestHandler.HandleUnloadChuckAsync(request);
        }
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