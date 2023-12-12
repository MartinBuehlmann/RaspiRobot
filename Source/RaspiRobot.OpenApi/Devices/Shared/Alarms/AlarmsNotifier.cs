namespace RaspiRobot.OpenApi.Devices.Shared.Alarms;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Alarms;
using Alarm = Erowa.OpenAPI.Alarm;

internal class AlarmsNotifier : IAlarmsNotifier
{
    private readonly AlarmConverter alarmConverter;
    private readonly IServerStreamWriter<AlarmResponse> responseStream;

    public AlarmsNotifier(
        AlarmConverter alarmConverter,
        IServerStreamWriter<AlarmResponse> responseStream)
    {
        this.alarmConverter = alarmConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(IReadOnlyList<RobotControl.Devices.Alarms.Alarm> alarms)
    {
        IReadOnlyList<Alarm> deviceAlarms = alarms.Select(x => this.alarmConverter.Convert(x)).ToList();
        await this.responseStream.WriteAsync(
            new AlarmResponse
            {
                Alarms = { deviceAlarms },
            });
    }
}