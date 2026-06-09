namespace RaspiRobot.RobotControl.GrabIt.Devices.Alarms;

using System;
using System.Collections.Generic;
using System.Linq;
using RaspiRobot.RobotControl.Devices.Alarms;

internal class AlarmsFacade : IAlarmsFacade
{
    private readonly Dictionary<string, AlarmData> alarms = new();

    public AlarmData[] RetrieveAlarms()
        => this.alarms.Values.ToArray();

    public void UpdateAlarmActivation(string code, bool isActive)
        => this.alarms[code] = this.alarms[code] with { IsActive = isActive, DateTime = DateTimeOffset.UtcNow };

    public void AddAlarm(AlarmData alarm)
        => this.alarms.Add(alarm.Code, alarm);
}