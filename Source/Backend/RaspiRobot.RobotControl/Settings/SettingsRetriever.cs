﻿namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Settings;

internal class SettingsRetriever : ISettingsRetriever
{
    private readonly CellSettingsLoader cellSettingsLoader;
    private CellSettings? cellSettings;

    public SettingsRetriever(CellSettingsLoader cellSettingsLoader)
    {
        this.cellSettingsLoader = cellSettingsLoader;
    }

    public async Task<RobotSettings> RetrieveRobotSettingsAsync()
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Robot;
    }

    public async Task<IReadOnlyList<AutoLinkMagazineSettings>> RetrieveAutoLinkMagazineSettingsAsync()
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.AutoLinkMagazines;
    }

    public async Task<IReadOnlyList<MagazineSettings>> RetrieveMagazineSettingsAsync()
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Magazines;
    }

    public async Task<IReadOnlyList<LoadingStationSettings>> RetrieveLoadingStationSettingsAsync()
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.LoadingStations;
    }

    public async Task<ChuckSettings> RetrieveByAsync(MachineChuck chuck)
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Machines
            .SelectMany(x => x.Chucks)
            .Single(c => c.Number == chuck.Number);
    }

    public async Task<PlaceSettings> RetrieveByAsync(StoragePlace place)
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.AutoLinkMagazines.SelectMany(x => x.Places)
            .Concat(this.cellSettings!.LoadingStations.SelectMany(x => x.Places))
            .Concat(this.cellSettings!.Magazines.SelectMany(x => x.Places))
            .Single(p => p.Number == place.Number);
    }

    private async Task EnsureCellSettingsLoadedAsync()
    {
        if (this.cellSettings is null)
        {
            this.cellSettings = await this.cellSettingsLoader.RetrieveOrCreateAsync(nameof(CellSettings));
        }
    }
}