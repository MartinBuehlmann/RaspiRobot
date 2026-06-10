namespace RaspiRobot.RobotControl.Devices.Alarms;

using System;

public interface IAlarmsFacade
{
    event Action? AlarmChanged;

    AlarmData[] RetrieveAlarms();

    bool UpdateAlarmActivation(string code, bool isActive);

    void AddAlarm(AlarmData alarm);
}