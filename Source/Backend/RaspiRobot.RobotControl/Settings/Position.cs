namespace RaspiRobot.RobotControl.Settings;

public class Position(byte drive, int value)
{
    public byte Drive { get; } = drive;

    public int Value { get; } = value;
}