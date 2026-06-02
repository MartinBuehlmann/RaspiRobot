namespace RaspiRobot.OpenApi.Devices.Robot.Alarms;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI.Robot.Alarm;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Alarms;
using Alarm = Erowa.OpenAPI.Robot.Alarm.Alarm;

internal class RobotAlarmsNotifier : IAlarmsNotifier
{
    private readonly RobotAlarmConverter robotAlarmConverter;
    private readonly IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream;

    public RobotAlarmsNotifier(
        RobotAlarmConverter robotAlarmConverter,
        IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream)
    {
        this.robotAlarmConverter = robotAlarmConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(IReadOnlyList<RobotControl.Devices.Alarms.Alarm> alarms)
    {
        IReadOnlyList<Alarm> deviceAlarms = alarms.Select(x => this.robotAlarmConverter.Convert(x)).ToList();
        await this.responseStream.WriteAsync(
            new RetrieveAlarmsChangedResponse
            {
                Alarms = { deviceAlarms },
            });
    }
}