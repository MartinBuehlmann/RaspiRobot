namespace RaspiRobot.Web.Features.Devices;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Settings;

public class DevicesRetriever
{
    private readonly ISettingsRetriever settingsRetriever;

    public DevicesRetriever(ISettingsRetriever settingsRetriever)
    {
        this.settingsRetriever = settingsRetriever;
    }

    public async Task<Devices> RetrieveAllAsync()
    {
        return new Devices(
            await this.RetrieveRobotDeviceAsync(),
            await this.RetrieveStorageDevicesAsync());
    }

    private async Task<Device> RetrieveRobotDeviceAsync()
    {
        RobotSettings robotSettings = await this.settingsRetriever.RetrieveRobotSettingsAsync();
        return new Device(robotSettings.Name, DeviceType.Robot);
    }

    private async Task<Device[]> RetrieveStorageDevicesAsync()
    {
        IReadOnlyList<LoadingStationSettings> loadingStationSettings =
            await this.settingsRetriever.RetrieveLoadingStationSettingsAsync();
        IReadOnlyList<AutoLinkMagazineSettings> autoLinkMagazineSettings =
            await this.settingsRetriever.RetrieveAutoLinkMagazineSettingsAsync();
        IReadOnlyList<MagazineSettings> magazineSettings = await this.settingsRetriever.RetrieveMagazineSettingsAsync();

        return loadingStationSettings.Select(x => new Device(x.Name, DeviceType.LoadingStation))
            .Concat(autoLinkMagazineSettings.Select(x => new Device(x.Name, DeviceType.AutoLinkMagazine)))
            .Concat(magazineSettings.Select(x => new Device(x.Name, DeviceType.Magazine)))
            .ToArray();
    }
}