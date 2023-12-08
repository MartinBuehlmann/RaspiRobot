namespace RaspiRobot.RobotControl.Devices.Magazine;

using System.Threading.Tasks;

public interface IStorageStateNotifier
{
    Task NotifyAsync(State state);
}