namespace RaspiRobot.OpenApi.Devices.Storages.Magazine;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

public class MagazineService : Erowa.OpenAPI.Storage.MagazineService.MagazineServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public MagazineService(
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
        IServerStreamWriter<MagazineStateResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var magazineStateNotifier = this.factory.Create<IMagazineStateNotifier>(responseStream);
        IMagazine magazine = this.deviceService.RetrieveMagazine(request.Number);
        await magazine.SubscribeForStateChangedAsync(magazineStateNotifier, cancellationTokenSource.Token);
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