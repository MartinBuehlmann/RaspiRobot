namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Settings;
using RaspiRobot.RobotControl.Settings;

internal class TransportSequenceBuilder
{
    public IReadOnlyList<Sequence> LoadChuckSequence(
        PlaceSettings place,
        ChuckSettings chuck,
        RobotSettings robot)
        => new List<Sequence>
        {
            place.PickSequence,
            robot.TransferSequence,
            chuck.PlaceSequence,
            robot.TransferSequence,
        };

    public IReadOnlyList<Sequence> UnloadChuckSequence(
        ChuckSettings chuck,
        PlaceSettings place,
        RobotSettings robot)
        => new List<Sequence>
        {
            chuck.PickSequence,
            robot.TransferSequence,
            place.PlaceSequence,
            robot.TransferSequence,
        };

    public IReadOnlyList<Sequence> ExchangePlaceSequence(
        PlaceSettings sourcePlace,
        PlaceSettings destinationPlace,
        RobotSettings robot)
        => new List<Sequence>
        {
            sourcePlace.PickSequence,
            robot.TransferSequence,
            destinationPlace.PlaceSequence,
            robot.TransferSequence,
        };
}