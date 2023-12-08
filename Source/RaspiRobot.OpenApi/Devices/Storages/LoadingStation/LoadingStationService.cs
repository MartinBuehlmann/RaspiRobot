namespace RaspiRobot.OpenApi.Devices.Storages.LoadingStation;

using System.Threading;
using System.Threading.Tasks;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.Common.DependencyInjection;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Magazine;

public class LoadingStationService : Erowa.OpenAPI.Storage.LoadingStationService.LoadingStationServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public LoadingStationService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveStateAndStateChanged(
        StorageRequest request,
        IServerStreamWriter<LoadingStationStateResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var loadingStationStateNotifier = this.factory.Create<ILoadingStationStateNotifier>(responseStream);
        IMagazine magazine = this.deviceService.RetrieveMagazine(request.Number);
        await magazine.SubscribeForStateChangedAsync(loadingStationStateNotifier, cancellationTokenSource.Token);
    }

    public override async Task RetrieveAlarmsAndAlarmsChanged(
        StorageRequest request,
        IServerStreamWriter<AlarmResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var alarmsNotifier = this.factory.Create<IAlarmsNotifier>(responseStream);
        IMagazine magazine = this.deviceService.RetrieveMagazine(request.Number);
        await magazine.SubscribeForAlarmsChangedAsync(alarmsNotifier, cancellationTokenSource.Token);
    }
}