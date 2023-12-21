namespace RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;

public class AutoLinkMagazineService : Erowa.OpenAPI.Storage.AutoLinkMagazineService.AutoLinkMagazineServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public AutoLinkMagazineService(
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
        IServerStreamWriter<AutoLinkMagazineStateResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var magazineStateNotifier = this.factory.Create<IAutoLinkMagazineStateNotifier>(responseStream);
        IAutoLinkMagazine autoLinkMagazine = this.deviceService.RetrieveAutoLinkMagazine(request.Number);
        await autoLinkMagazine.SubscribeForStateChangedAsync(magazineStateNotifier, cancellationTokenSource.Token);
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
        IAutoLinkMagazine autoLinkMagazine = this.deviceService.RetrieveAutoLinkMagazine(request.Number);
        await autoLinkMagazine.SubscribeForAlarmsChangedAsync(alarmsNotifier, cancellationTokenSource.Token);
    }
}