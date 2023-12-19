namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System;
using System.Collections.Generic;
using RaspiRobot.RobotControl.GrabIt.Settings;

internal class TransportSequenceStepInterpolator
{
    public IReadOnlyList<GrabItPosition> Interpolate(
        IReadOnlyList<GrabItPosition> positions,
        IReadOnlyDictionary<byte, int> currentDrivePositions)
    {
        int stepCount = 1;
        foreach (GrabItPosition position in positions)
        {
            stepCount = (int)(Math.Max(stepCount, Math.Abs(position.Value - currentDrivePositions[position.Drive])) * 0.8);
        }

        var interpolatedPositions = new List<GrabItPosition>();
        for (int i = 1; i <= stepCount; i++)
        {
            foreach (GrabItPosition position in positions)
            {
                int interpolatedValue = currentDrivePositions[position.Drive] +
                                        ((position.Value - currentDrivePositions[position.Drive]) * i / stepCount);
                var interpolatedPosition = new GrabItPosition(position.Drive, interpolatedValue);
                interpolatedPositions.Add(interpolatedPosition);
            }
        }

        return interpolatedPositions;
    }
}