namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System;
using System.Collections.Generic;
using RaspiRobot.RobotControl.Devices.Robot.Steps;

internal class TransportSequenceStepInterpolator
{
    public IReadOnlyList<Position> Interpolate(
        IReadOnlyList<Position> positions,
        IReadOnlyDictionary<byte, int> currentDrivePositions)
    {
        int stepCount = 1;
        foreach (Position position in positions)
        {
            stepCount = (int)(Math.Max(stepCount, Math.Abs(position.Value - currentDrivePositions[position.Drive])) * 0.8);
        }

        var interpolatedPositions = new List<Position>();
        for (int i = 1; i <= stepCount; i++)
        {
            foreach (Position position in positions)
            {
                int interpolatedValue = currentDrivePositions[position.Drive] +
                                        ((position.Value - currentDrivePositions[position.Drive]) * i / stepCount);
                var interpolatedPosition = new Position(position.Drive, interpolatedValue);
                interpolatedPositions.Add(interpolatedPosition);
            }
        }

        return interpolatedPositions;
    }
}