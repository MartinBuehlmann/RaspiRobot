namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RaspiRobot.RobotControl.GrabIt.Settings;
using RaspiRobot.RobotControl.Settings;

internal class SequencesExecutor
{
    public void Execute(IReadOnlyList<Sequence> sequences, IGrabItDriver driver)
    {
        foreach (Sequence sequence in sequences)
        {
            foreach (Step step in sequence.Steps)
            {
                IReadOnlyDictionary<byte, int> currentDrivePositions = driver.CurrentDrivePositions;
                IReadOnlyList<GrabItPosition> positions = step.Positions
                    .OfType<GrabItPosition>()
                    .ToList();

                if (currentDrivePositions.Any())
                {
                    positions = this.Interpolate(
                        positions,
                        currentDrivePositions);
                }

                driver.Execute(positions);

                Thread.Sleep(10);
            }

            Thread.Sleep(150);
        }
    }

    private IReadOnlyList<GrabItPosition> Interpolate(
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