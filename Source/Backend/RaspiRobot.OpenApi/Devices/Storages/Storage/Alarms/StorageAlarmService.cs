namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Storage.Alarm;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Storages;

internal class StorageAlarmService : Erowa.OpenAPI.Storage.Alarm.StorageAlarmService.StorageAlarmServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public StorageAlarmService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveAlarmsChanged(
        RetrieveAlarmsChangedRequest request,
        IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream,
        ServerCallContext context)
    {
        CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var storageAlarmsNotifier = this.factory.Create<StorageAlarmsNotifier>(responseStream);
        IStorage storage = this.deviceService.RetrieveStorage<IStorage>(
            int.Parse(request.Storage.Identifier, CultureInfo.InvariantCulture));
        await storage.SubscribeForAlarmsChangedAsync(storageAlarmsNotifier, cancellationTokenSource.Token);
    }
}