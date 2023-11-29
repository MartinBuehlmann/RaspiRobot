namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChuckLoadingsNotifier
{
    Task NotifyAsync(IReadOnlyList<ChuckLoading> chuckLoadings);
}