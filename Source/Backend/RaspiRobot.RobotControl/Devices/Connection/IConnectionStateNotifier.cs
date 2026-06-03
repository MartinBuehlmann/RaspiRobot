namespace RaspiRobot.RobotControl.Devices.Connection;

using System.Threading.Tasks;

public interface IConnectionStateNotifier
{
    Task NotifyAsync(bool isConnected);
}