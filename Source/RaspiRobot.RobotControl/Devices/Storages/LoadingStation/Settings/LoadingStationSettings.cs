namespace RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Devices.Storages.Settings;

public class LoadingStationSettings
{
    public LoadingStationSettings(int number, string name)
    {
        this.Number = number;
        this.Name = name;
    }

    public int Number { get; set; }

    public string Name { get; }

    public List<PlaceSettings> Places { get; } = new();
}