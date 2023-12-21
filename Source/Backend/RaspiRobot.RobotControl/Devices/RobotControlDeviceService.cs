namespace RaspiRobot.RobotControl.Devices;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

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

    public IAutoLinkMagazine RetrieveAutoLinkMagazine(int number)
    {
        return this.deviceRegistry.Retrieve((IAutoLinkMagazine autoLinkMagazine) => autoLinkMagazine.Number == number);
    }

    public ILoadingStation RetrieveLoadingStation(int number)
    {
        return this.deviceRegistry.Retrieve((ILoadingStation loadingStation) => loadingStation.Number == number);
    }

    public IMagazine RetrieveMagazine(int number)
    {
        return this.deviceRegistry.Retrieve((IMagazine magazine) => magazine.Number == number);
    }
}