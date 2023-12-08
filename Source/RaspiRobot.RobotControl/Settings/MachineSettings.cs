namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class MachineSettings
{
    public MachineSettings(string name)
    {
        this.Name = name;
    }

    public string Name { get; }

    public List<ChuckSettings> Chucks { get; } = new();
}