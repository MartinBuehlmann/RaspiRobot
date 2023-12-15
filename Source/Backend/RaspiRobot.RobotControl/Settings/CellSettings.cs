namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;

public class CellSettings
{
    public CellSettings(RobotSettings robot)
    {
        this.Robot = robot;
    }

    public RobotSettings Robot { get; }

    public List<MachineSettings> Machines { get; } = new();

    public List<LoadingStationSettings> LoadingStations { get; } = new();

    public List<MagazineSettings> Magazines { get; } = new();

    public List<AutoLinkMagazineSettings> AutoLinkMagazines { get; } = new();
}