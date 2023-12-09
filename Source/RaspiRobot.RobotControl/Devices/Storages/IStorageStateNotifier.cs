namespace RaspiRobot.RobotControl.Devices.Storages;

using System.Threading.Tasks;

public interface IStorageStateNotifier
{
    Task NotifyAsync(State state);
}