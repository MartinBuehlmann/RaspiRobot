namespace RaspiRobot.RobotControl.Devices.Alarms;

public interface IAlarmsFacade
{
    AlarmData[] RetrieveAlarms();

    bool UpdateAlarmActivation(string code, bool isActive);

    void AddAlarm(AlarmData alarm);
}