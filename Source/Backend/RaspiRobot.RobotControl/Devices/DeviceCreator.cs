namespace RaspiRobot.RobotControl.Devices;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Common.Logging;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;
using RaspiRobot.RobotControl.Settings;

internal class DeviceCreator
{
    private readonly CellSettingsLoader cellSettingsLoader;
    private readonly Factory factory;
    private readonly Log log;

    public DeviceCreator(
        CellSettingsLoader cellSettingsLoader,
        Factory factory,
        Log log)
    {
        this.cellSettingsLoader = cellSettingsLoader;
        this.factory = factory;
        this.log = log;
    }

    public async Task<IReadOnlyList<IDevice>> CreateAllAsync()
    {
        var devices = new List<IDevice>();
        CellSettings cellSettings = await this.cellSettingsLoader.RetrieveOrCreateAsync(nameof(CellSettings));

        devices.Add(this.CreateDevice<IRobot>(cellSettings.Robot));
        devices.AddRange(cellSettings.Machines.Select(this.CreateDevice<IMachine>));
        devices.AddRange(cellSettings.AutoLinkMagazines.Select(this.CreateDevice<IAutoLinkMagazine>));
        devices.AddRange(cellSettings.LoadingStations.Select(this.CreateDevice<ILoadingStation>));
        devices.AddRange(cellSettings.Magazines.Select(this.CreateDevice<IMagazine>));

        return devices;
    }

    private TDevice CreateDevice<TDevice>(object deviceSettings)
        where TDevice : notnull
    {
        this.log.Verbose(
            "Create device of type '{DeviceType}' with settings '{@DeviceSettings}'",
            typeof(TDevice),
            deviceSettings);
        return this.factory.Create<TDevice>(deviceSettings);
    }
}