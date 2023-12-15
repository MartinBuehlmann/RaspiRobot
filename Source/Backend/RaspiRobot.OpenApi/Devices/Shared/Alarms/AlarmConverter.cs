namespace RaspiRobot.OpenApi.Devices.Shared.Alarms;

using System;
using Erowa.OpenAPI;

internal class AlarmConverter
{
    public Alarm Convert(RobotControl.Devices.Alarms.Alarm alarm)
    {
        return new Alarm { Code = alarm.Code, Message = alarm.Message, Severity = this.Convert(alarm.Severity) };
    }

    private Severity Convert(RobotControl.Devices.Alarms.Severity severity)
    {
        return severity switch
        {
            RobotControl.Devices.Alarms.Severity.Error => Severity.Error,
            RobotControl.Devices.Alarms.Severity.Information => Severity.Information,
            RobotControl.Devices.Alarms.Severity.Warning => Severity.Warning,
            _ => throw new NotSupportedException($"Invalid alarm severity '{severity}' detected."),
        };
    }
}