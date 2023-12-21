namespace RaspiRobot.RobotControl.Devices.Robot.State;

using System.Threading.Tasks;

public interface IRobotStateNotifier
{
    Task NotifyAsync(RobotState robotState);
}