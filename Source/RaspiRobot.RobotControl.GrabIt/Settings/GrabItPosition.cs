namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Settings;

public class GrabItPosition : IPosition
{
    public GrabItPosition(int drive, int value)
    {
        this.Drive = drive;
        this.Value = value;
    }

    public int Drive { get; }

    public int Value { get; }
}