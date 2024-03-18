namespace RaspiRobot.RobotControl.Devices.Machines.Settings;

using RaspiRobot.RobotControl.Settings;

public class ChuckSettings
{
    public ChuckSettings(int number, SequenceSettings pickSequence, SequenceSettings placeSequence)
    {
        this.Number = number;
        this.PickSequence = pickSequence;
        this.PlaceSequence = placeSequence;
    }

    public int Number { get; }

    public SequenceSettings PickSequence { get; }

    public SequenceSettings PlaceSequence { get; }
}