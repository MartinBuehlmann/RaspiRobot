namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class MagazineSettings
{
    public MagazineSettings(int number, string name)
    {
        this.Number = number;
        this.Name = name;
    }

    public int Number { get; set; }

    public string Name { get; }

    public List<PlaceSettings> Places { get; } = new();
}