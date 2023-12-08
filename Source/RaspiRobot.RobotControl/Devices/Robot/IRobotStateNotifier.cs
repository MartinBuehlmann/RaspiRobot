namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Threading.Tasks;

public interface IRobotStateNotifier
{
    Task NotifyAsync(RobotControl.Devices.Robot.State state);
}