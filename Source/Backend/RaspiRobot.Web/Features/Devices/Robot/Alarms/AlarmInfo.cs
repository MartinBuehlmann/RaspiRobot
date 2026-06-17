namespace RaspiRobot.Web.Features.Devices.Robot.Alarms;

using System;

public record AlarmInfo(string Code, string Message, DateTimeOffset DateTime, Severity Severity, bool IsActive);