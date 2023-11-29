namespace RaspiRobot.RobotControl.Devices.Magazine;

using System.Threading.Tasks;

public interface IMagazineStateNotifier
{
    Task NotifyAsync(RobotControl.Devices.Magazine.State state);
}