namespace RaspiRobot.RobotControl.Devices.Alarms;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlarmsNotifier
{
    Task NotifyAsync(IReadOnlyList<Alarm> alarms);
}