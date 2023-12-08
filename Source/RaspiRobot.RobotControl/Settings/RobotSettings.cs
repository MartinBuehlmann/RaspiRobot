namespace RaspiRobot.RobotControl.Settings;

public class RobotSettings
{
    public RobotSettings(string name, Sequence homingSequence, Sequence transferSequence)
    {
        this.Name = name;
        this.HomingSequence = homingSequence;
        this.TransferSequence = transferSequence;
    }

    public string Name { get; }

    public Sequence HomingSequence { get; }

    public Sequence TransferSequence { get; }
}