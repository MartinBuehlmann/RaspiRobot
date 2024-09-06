namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System;
using System.Collections.Generic;
using System.Linq;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Steps;
using RaspiRobot.RobotControl.Devices.Storages;
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
            BuildChuckLoadingSequence(place.PickSequence, chuck, place),
            BuildChuckLoadingSequence(robot.TransferSequence, chuck, place),
            BuildChuckLoadingSequence(chuck.PlaceSequence, chuck, place),
            BuildChuckLoadingSequence(robot.TransferSequence, chuck, place),
        };

    public IReadOnlyList<Sequence> UnloadChuckSequence(
        ChuckSettings chuck,
        PlaceSettings place,
        RobotSettings robot)
        => new List<Sequence>
        {
            BuildChuckUnloadingSequence(chuck.PickSequence, chuck),
            BuildChuckUnloadingSequence(robot.TransferSequence, chuck),
            BuildChuckUnloadingSequence(place.PlaceSequence, chuck),
            BuildChuckUnloadingSequence(robot.TransferSequence, chuck),
        };

    public IReadOnlyList<Sequence> ExchangePlaceSequence(
        PlaceSettings sourcePlace,
        PlaceSettings destinationPlace,
        RobotSettings robot)
        => new List<Sequence>
        {
            BuildMoveOnlySequence(sourcePlace.PickSequence),
            BuildMoveOnlySequence(robot.TransferSequence),
            BuildMoveOnlySequence(destinationPlace.PlaceSequence),
            BuildMoveOnlySequence(robot.TransferSequence),
        };

    public IReadOnlyList<Sequence> HomingSequence(RobotSettings robot)
        => new List<Sequence>
        {
            BuildMoveOnlySequence(robot.HomingSequence),
        };

    private static Sequence BuildChuckLoadingSequence(
        SequenceSettings sequence,
        ChuckSettings chuck,
        PlaceSettings place)
        => new Sequence(
            sequence.Steps.Select(x =>
                {
                    return x switch
                    {
                        MoveStepSettings moveStepSettings => BuildMoveStep(moveStepSettings),
                        ChuckLoadingChangedNotificationStepSettings => ChuckLoadingChangedNotificationStep.Occupied(
                            new MachineChuck(chuck.Number),
                            new PalletChuckOccupancy(new StoragePlace(place.Number), null)),
                        _ => throw new NotSupportedException($"Step of type '{x.GetType()}' is not supported."),
                    };
                })
                .ToArray());

    private static Sequence BuildChuckUnloadingSequence(
        SequenceSettings sequence,
        ChuckSettings chuck)
        => new Sequence(
            sequence.Steps.Select(x =>
                {
                    return x switch
                    {
                        MoveStepSettings moveStepSettings => BuildMoveStep(moveStepSettings),
                        ChuckLoadingChangedNotificationStepSettings => ChuckLoadingChangedNotificationStep.Empty(
                            new MachineChuck(chuck.Number)),
                        _ => throw new NotSupportedException($"Step of type '{x.GetType()}' is not supported."),
                    };
                })
                .ToArray());

    private static Sequence BuildMoveOnlySequence(SequenceSettings sequence)
        => new Sequence(
            sequence.Steps.Select(x =>
                {
                    return x switch
                    {
                        MoveStepSettings moveStepSettings => BuildMoveStep(moveStepSettings),
                        _ => throw new NotSupportedException($"Step of type '{x.GetType()}' is not supported."),
                    };
                })
                .ToArray());

    private static IStep BuildMoveStep(MoveStepSettings moveStepSettings)
        => new MoveStep(moveStepSettings.Positions.Select(BuildPosition).ToArray());

    private static Position BuildPosition(PositionSettings positionSettings)
        => new Position(positionSettings.Drive, positionSettings.Value);
}