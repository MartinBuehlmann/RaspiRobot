namespace RaspiRobot.RobotControl.Settings;

public class ChuckSettings
{
    public ChuckSettings(int number, Sequence pickSequence, Sequence placeSequence)
    {
        this.Number = number;
        this.PickSequence = pickSequence;
        this.PlaceSequence = placeSequence;
    }

    public int Number { get; }

    public Sequence PickSequence { get; }

    public Sequence PlaceSequence { get; }
}