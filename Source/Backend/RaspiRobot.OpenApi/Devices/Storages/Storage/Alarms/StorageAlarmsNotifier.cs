namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI.Storage.Alarm;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Alarms;
using Alarm = Erowa.OpenAPI.Storage.Alarm.Alarm;

internal class StorageAlarmsNotifier : IAlarmsNotifier
{
    private readonly StorageAlarmConverter storageAlarmConverter;
    private readonly IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream;

    public StorageAlarmsNotifier(
        StorageAlarmConverter storageAlarmConverter,
        IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream)
    {
        this.storageAlarmConverter = storageAlarmConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(IReadOnlyList<RobotControl.Devices.Alarms.Alarm> alarms)
    {
        IReadOnlyList<Alarm> deviceAlarms = alarms.Select(x => this.storageAlarmConverter.Convert(x)).ToList();
        await this.responseStream.WriteAsync(
            new RetrieveAlarmsChangedResponse
            {
                Alarms = { deviceAlarms },
            });
    }
}