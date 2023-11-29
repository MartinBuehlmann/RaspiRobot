namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class MagazineSettings
{
    public MagazineSettings(int number, string name, Position position)
    {
        this.Number = number;
        this.Name = name;
        this.Position = position;
    }

    public int Number { get; set; }

    public string Name { get; }

    public Position Position { get; }

    public List<PlaceSettings> Places { get; } = new();
}