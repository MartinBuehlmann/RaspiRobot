namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Alarms;

using System;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl.Devices.Alarms;
using Alarm = Erowa.OpenAPI.Storage.Alarm.Alarm;

internal class StorageAlarmConverter
{
    public Alarm Convert(RobotControl.Devices.Alarms.Alarm robotAlarm)
    {
        var alarm = new Alarm
        {
            Code = robotAlarm.Code,
            Message = robotAlarm.Message,
            CreationTimestamp = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow),
        };

        switch (robotAlarm.Severity)
        {
            case Severity.Error:
                alarm.Critical = new Empty();
                break;
            case Severity.Information:
                alarm.Information = new Empty();
                break;
            case Severity.Warning:
                alarm.Warning = new Empty();
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(robotAlarm.Severity),
                    robotAlarm.Severity,
                    "Unknown severity");
        }

        return alarm;
    }
}