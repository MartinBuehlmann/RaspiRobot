namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

public class TransportSequenceBuilder
{
    public IReadOnlyList<Sequence> LoadChuckSequence(PlaceSettings place, ChuckSettings chuck, RobotSettings robot)
    {
        var sequence = new List<Sequence>();
        sequence.Add(place.PickSequence);
        sequence.Add(robot.TransferSequence);
        sequence.Add(chuck.PlaceSequence);
        sequence.Add(robot.TransferSequence);
        return sequence;
    }

    public IReadOnlyList<Sequence> UnloadChuckSequence(ChuckSettings chuck, PlaceSettings place, RobotSettings robot)
    {
        var sequence = new List<Sequence>();
        sequence.Add(chuck.PickSequence);
        sequence.Add(robot.TransferSequence);
        sequence.Add(place.PlaceSequence);
        sequence.Add(robot.TransferSequence);
        return sequence;
    }

    public IReadOnlyList<Sequence> ExchangePlaceSequence(PlaceSettings sourcePlace, PlaceSettings destinationPlace, RobotSettings robot)
    {
        var sequence = new List<Sequence>();
        sequence.Add(sourcePlace.PickSequence);
        sequence.Add(robot.TransferSequence);
        sequence.Add(destinationPlace.PlaceSequence);
        sequence.Add(robot.TransferSequence);
        return sequence;
    }
}