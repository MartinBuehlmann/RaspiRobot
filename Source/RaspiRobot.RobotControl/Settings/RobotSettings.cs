namespace RaspiRobot.RobotControl.Settings;

public class RobotSettings
{
    public RobotSettings(string name, Sequence sequence)
    {
        this.Name = name;
        this.Sequence = sequence;
    }

    public string Name { get; }

    public Sequence Sequence { get; }
}