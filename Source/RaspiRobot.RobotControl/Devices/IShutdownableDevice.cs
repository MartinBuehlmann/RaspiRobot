namespace RaspiRobot.RobotControl.Devices;

using System.Threading;
using System.Threading.Tasks;

public interface IShutdownableDevice
{
    Task ShutdownAsync(CancellationToken cancellationToken);
}