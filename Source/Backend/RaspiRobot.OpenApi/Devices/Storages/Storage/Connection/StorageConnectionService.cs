namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Storage.Connection;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Storages;

internal class
    StorageConnectionService : Erowa.OpenAPI.Storage.Connection.StorageConnectionService.StorageConnectionServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public StorageConnectionService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveConnectionStateChanged(
        RetrieveConnectionStateChangedRequest request,
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var connectionStateNotifier = this.factory.Create<StorageConnectionStateNotifier>(responseStream);
        IStorage storage = this.deviceService.RetrieveStorage<IStorage>(
            int.Parse(request.Storage.Identifier, CultureInfo.InvariantCulture));
        await storage.SubscribeForConnectionStateChangedAsync(connectionStateNotifier, cancellationTokenSource.Token);
    }
}