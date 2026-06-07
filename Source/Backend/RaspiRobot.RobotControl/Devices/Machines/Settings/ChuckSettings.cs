namespace RaspiRobot.RobotControl.Devices.Machines.Settings;

using RaspiRobot.RobotControl.Settings;

public class ChuckSettings
{
    public ChuckSettings(string identifier, SequenceSettings pickSequence, SequenceSettings placeSequence)
    {
        this.Identifier = identifier;
        this.PickSequence = pickSequence;
        this.PlaceSequence = placeSequence;
    }

    public string Identifier { get; }

    public SequenceSettings PickSequence { get; }

    public SequenceSettings PlaceSequence { get; }
}