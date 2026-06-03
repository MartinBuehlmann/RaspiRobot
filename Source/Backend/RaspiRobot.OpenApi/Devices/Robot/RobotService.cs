namespace RaspiRobot.OpenApi.Devices.Robot;

using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Robot;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.OpenApi.Communication;
using RaspiRobot.OpenApi.Devices.Robot.State;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Robot;
using StoragePlace = RaspiRobot.RobotControl.Devices.Storages.StoragePlace;

internal class RobotService : Erowa.OpenAPI.Robot.RobotService.RobotServiceBase
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

    public override async Task RetrieveStateChanged(
        Empty request,
        IServerStreamWriter<RetrieveStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var robotStateNotifier = this.factory.Create<RobotStateNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForStateChangedAsync(robotStateNotifier, cancellationTokenSource.Token);
    }

    public override async Task<LoadChuckResponse> LoadChuck(
        IAsyncStreamReader<LoadChuckRequest> requestStream,
        ServerCallContext context)
    {
        LoadChuckResponse[] results = await this.grpcStreamListener.ListenAsync(
            requestStream,
            async request => await HandleLoadChuckRequestAsync(request),
            context.Peer,
            context.Method,
            context.CancellationToken);

        return results.SingleOrDefault() ??
               new LoadChuckResponse
               {
                   Unsuccessful = new Unsuccessful
                   {
                       Message = "Command was not started",
                   },
               };

        async Task<LoadChuckResponse> HandleLoadChuckRequestAsync(LoadChuckRequest loadChuckRequest)
        {
            var rollbackCancellationTokenSource = new CancellationTokenSource();
            switch (loadChuckRequest.RequestCase)
            {
                case LoadChuckRequest.RequestOneofCase.Start:
                    return await this.startLoadChuckRequestHandler.HandleLoadChuckAsync(
                        loadChuckRequest.Start,
                        rollbackCancellationTokenSource.Token);
                case LoadChuckRequest.RequestOneofCase.Rollback:
                    await rollbackCancellationTokenSource.CancelAsync();
                    return new LoadChuckResponse { Successful = new Empty() };
                default:
                    throw new NotSupportedException(
                        $"Load chuck request of type '{loadChuckRequest.RequestCase}' is not supported.");
            }
        }
    }

    public override async Task<UnloadChuckResponse> UnloadChuck(
        IAsyncStreamReader<UnloadChuckRequest> requestStream,
        ServerCallContext context)
    {
        UnloadChuckResponse[] results = await this.grpcStreamListener.ListenAsync(
            requestStream,
            async request => await HandleUnloadChuckRequestAsync(request),
            context.Peer,
            context.Method,
            context.CancellationToken);

        return results.SingleOrDefault() ??
               new UnloadChuckResponse
               {
                   Unsuccessful = new Unsuccessful
                   {
                       Message = "Command was not started",
                   },
               };

        async Task<UnloadChuckResponse> HandleUnloadChuckRequestAsync(UnloadChuckRequest unloadChuckRequest)
        {
            var rollbackCancellationTokenSource = new CancellationTokenSource();
            switch (unloadChuckRequest.RequestCase)
            {
                case UnloadChuckRequest.RequestOneofCase.Start:
                    return await this.startUnloadChuckRequestHandler.HandleUnloadChuckAsync(
                        unloadChuckRequest.Start,
                        rollbackCancellationTokenSource.Token);
                case UnloadChuckRequest.RequestOneofCase.Rollback:
                    await rollbackCancellationTokenSource.CancelAsync();
                    return new UnloadChuckResponse { Successful = new Empty() };
                default:
                    throw new NotSupportedException(
                        $"Unload chuck request of type '{unloadChuckRequest.RequestCase}' is not supported.");
            }
        }
    }

    public override async Task<ExchangeStoragePlaceResponse> ExchangeStoragePlace(
        ExchangeStoragePlaceRequest request,
        ServerCallContext context)
    {
        IRobot robot = this.deviceService.RetrieveRobot();

        ICommandResponse response = await robot.ExchangeStoragePlaceAsync(
            new StoragePlace(int.Parse(request.SourcePlace.Identifier, CultureInfo.InvariantCulture)),
            new StoragePlace(int.Parse(request.DestinationPlace.Identifier, CultureInfo.InvariantCulture)));

        return CreateResponse(response);
    }

    private static ExchangeStoragePlaceResponse CreateResponse(ICommandResponse response)
        => response switch
        {
            ErrorResponse errorResponse => new ExchangeStoragePlaceResponse
                { Unsuccessful = new Unsuccessful { Message = errorResponse.Message } },

            SuccessResponse => new ExchangeStoragePlaceResponse { Successful = new Empty() },

            _ => throw new ArgumentOutOfRangeException(nameof(response), response, "Unknown response type"),
        };
}