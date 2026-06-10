namespace RaspiRobot.RobotControl.GrabIt.Devices.Alarms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;

internal class AlarmsFacade : IAlarmsFacade
{
    private readonly Lock lockObject = new();
    private readonly Dictionary<string, AlarmData> alarms = new();

    public event Action? AlarmChanged;

    public AlarmData[] RetrieveAlarms()
    {
        lock (this.lockObject)
        {
            return this.alarms.Values.ToArray();
        }
    }

    public bool UpdateAlarmActivation(string code, bool isActive)
    {
        lock (this.lockObject)
        {
            if (!this.alarms.ContainsKey(code))
            {
                return false;
            }

            this.alarms[code] = this.alarms[code] with { IsActive = isActive, DateTime = DateTimeOffset.UtcNow };
        }

        this.AlarmChanged?.Invoke();
        return true;
    }

    public void AddAlarm(AlarmData alarm)
    {
        lock (this.lockObject)
        {
            this.alarms.Add(alarm.Code, alarm);
        }
    }
}