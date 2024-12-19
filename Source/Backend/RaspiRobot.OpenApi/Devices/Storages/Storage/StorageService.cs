namespace RaspiRobot.OpenApi.Devices.Storages.Storage;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages;

internal class StorageService : Erowa.OpenAPI.Storage.StorageService.StorageServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public StorageService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveStateChanged(
        StorageRequest request,
        IServerStreamWriter<StorageStateResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var storageStateNotifier = this.factory.Create<IStorageStateNotifier>(responseStream);
        IStorage storage = this.deviceService.RetrieveStorage<IStorage>(request.Number);
        await storage.SubscribeForStateChangedAsync(storageStateNotifier, cancellationTokenSource.Token);
    }

    public override async Task RetrieveAlarmsChanged(
        StorageRequest request,
        IServerStreamWriter<AlarmsResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var alarmsNotifier = this.factory.Create<IAlarmsNotifier>(responseStream);
        IStorage storage = this.deviceService.RetrieveStorage<IStorage>(request.Number);
        await storage.SubscribeForAlarmsChangedAsync(alarmsNotifier, cancellationTokenSource.Token);
    }
}