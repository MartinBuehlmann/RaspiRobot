namespace RaspiRobot.RobotControl.Devices.Storages.State;

using System.Threading.Tasks;

public interface IStorageStateNotifier
{
    Task NotifyAsync(State state);
}