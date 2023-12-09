namespace RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Devices.Storages.Settings;

public class AutoLinkMagazineSettings
{
    public AutoLinkMagazineSettings(int number, string name)
    {
        this.Number = number;
        this.Name = name;
    }

    public int Number { get; set; }

    public string Name { get; }

    public List<PlaceSettings> Places { get; } = new();
}