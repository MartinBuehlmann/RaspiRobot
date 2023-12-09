namespace RaspiRobot.RobotControl.Devices.Storages.Settings;

using RaspiRobot.RobotControl.Settings;

public class PlaceSettings
{
    public PlaceSettings(int number, Sequence pickSequence, Sequence placeSequence)
    {
        this.Number = number;
        this.PickSequence = pickSequence;
        this.PlaceSequence = placeSequence;
    }

    public int Number { get; }

    public Sequence PickSequence { get; }

    public Sequence PlaceSequence { get; }
}