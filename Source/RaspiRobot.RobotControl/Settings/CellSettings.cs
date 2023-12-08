namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;

public class CellSettings
{
    public CellSettings(RobotSettings robot)
    {
        this.Robot = robot;
    }

    public RobotSettings Robot { get; }

    public List<MachineSettings> Machines { get; } = new();

    public List<MagazineSettings> Magazines { get; } = new();
}