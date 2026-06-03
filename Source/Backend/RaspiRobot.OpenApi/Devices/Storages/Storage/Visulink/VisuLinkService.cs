namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Visulink;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Storage.VisuLink;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.OpenApi.Devices.Storages.Storage.State;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Storages;

internal class VisuLinkService : Erowa.OpenAPI.Storage.VisuLink.VisuLinkService.VisuLinkServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public VisuLinkService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveStateChanged(
        RetrieveStateChangedRequest request,
        IServerStreamWriter<RetrieveStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var storageStateNotifier = this.factory.Create<StorageStateNotifier>(responseStream);
        IStorage storage = this.deviceService.RetrieveStorage<IStorage>(
            int.Parse(request.Storage.Identifier, CultureInfo.InvariantCulture));
        await storage.SubscribeForStateChangedAsync(storageStateNotifier, cancellationTokenSource.Token);
    }
}