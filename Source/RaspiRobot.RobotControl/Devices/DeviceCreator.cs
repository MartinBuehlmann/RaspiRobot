﻿namespace RaspiRobot.RobotControl.Devices;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.Common.DependencyInjection;
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

    public DeviceCreator(
        CellSettingsLoader cellSettingsLoader, Factory factory)
    {
        this.cellSettingsLoader = cellSettingsLoader;
        this.factory = factory;
    }

    public async Task<IReadOnlyList<IDevice>> CreateAllAsync()
    {
        var devices = new List<IDevice>();
        CellSettings cellSettings = await this.cellSettingsLoader.RetrieveOrCreateAsync(nameof(CellSettings));

        devices.Add(this.factory.Create<IRobot>(cellSettings.Robot));
        devices.AddRange(cellSettings.Machines.Select(x => this.factory.Create<IMachine>(x)));
        devices.AddRange(cellSettings.AutoLinkMagazines.Select(x => this.factory.Create<IAutoLinkMagazine>(x)));
        devices.AddRange(cellSettings.LoadingStations.Select(x => this.factory.Create<ILoadingStation>(x)));
        devices.AddRange(cellSettings.Magazines.Select(x => this.factory.Create<IMagazine>(x)));

        return devices;
    }
}