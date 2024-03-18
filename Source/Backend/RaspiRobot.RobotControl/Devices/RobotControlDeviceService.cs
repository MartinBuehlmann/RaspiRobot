namespace RaspiRobot.RobotControl.Devices;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages;

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
        return this.deviceRegistry.Retrieve((IRobot _) => true);
    }

    public TStorage RetrieveStorage<TStorage>(int number)
        where TStorage : IStorage
    {
        return this.deviceRegistry.Retrieve((TStorage storage) => storage.Number == number);
    }
}