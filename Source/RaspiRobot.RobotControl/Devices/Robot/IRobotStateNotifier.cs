namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Threading.Tasks;

public interface IRobotStateNotifier
{
    Task NotifyAsync(State state);
}