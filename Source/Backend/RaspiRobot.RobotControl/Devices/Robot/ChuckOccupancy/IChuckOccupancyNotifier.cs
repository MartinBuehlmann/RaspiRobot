namespace RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChuckOccupancyNotifier
{
    Task NotifyAsync(IReadOnlyList<ChuckOccupancy> chuckOccupancies);
}