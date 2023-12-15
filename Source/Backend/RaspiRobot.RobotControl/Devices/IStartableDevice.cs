namespace RaspiRobot.RobotControl.Devices;

using System.Threading;
using System.Threading.Tasks;

public interface IStartableDevice
{
    Task StartAsync(CancellationToken cancellationToken);
}