namespace RaspiRobot.RobotControl.Devices.Alarms;

using System;

public record AlarmData(string Code, string Message, Severity Severity, DateTimeOffset DateTime, bool IsActive)
    : Alarm(Code, Message, Severity);