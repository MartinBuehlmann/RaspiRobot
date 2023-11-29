namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class MachineSettings
{
    public MachineSettings(string name, Position position)
    {
        this.Name = name;
        this.Position = position;
    }

    public string Name { get; }

    public Position Position { get; }

    public List<ChuckSettings> Chucks { get; } = new();
}