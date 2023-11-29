namespace RaspiRobot.RobotControl.Devices;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Common;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Devices.Robot;

internal class RobotControlDeviceService : IBackgroundService, IDeviceService
{
    private readonly DeviceRegistry deviceRegistry;

    public RobotControlDeviceService(DeviceRegistry deviceRegistry)
    {
        this.deviceRegistry = deviceRegistry;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this.deviceRegistry.InitializeAsync();
        await Task.WhenAll(this.deviceRegistry.RetrieveAll<IStartableDevice>()
            .Select(x => x.StartAsync(cancellationToken)));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(this.deviceRegistry.RetrieveAll<IShutdownableDevice>()
            .Select(x => x.ShutdownAsync(cancellationToken)));
    }

    public IRobot RetrieveRobot()
    {
        return this.deviceRegistry.Retrieve((IRobot robot) => true);
    }

    public IMagazine RetrieveMagazine(int number)
    {
        return this.deviceRegistry.Retrieve((IMagazine magazine) => magazine.Number == number);
    }
}