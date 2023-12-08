namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Settings;

public class GrabItPosition : IPosition
{
    public GrabItPosition(byte drive, int value)
    {
        this.Drive = drive;
        this.Value = value;
    }

    public byte Drive { get; }

    public int Value { get; }
}