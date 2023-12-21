namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RaspiRobot.RobotControl.Settings;

internal class TransportSequenceExecutor
{
    private readonly TransportSequenceStepInterpolator interpolator;

    public TransportSequenceExecutor(TransportSequenceStepInterpolator interpolator)
    {
        this.interpolator = interpolator;
    }

    public void Execute(IReadOnlyList<Sequence> sequences, IGrabItDriver driver)
    {
        foreach (Sequence sequence in sequences)
        {
            foreach (Step step in sequence.Steps)
            {
                IReadOnlyDictionary<byte, int> currentDrivePositions = driver.CurrentDrivePositions;
                IReadOnlyList<Position> positions = step.Positions;

                if (currentDrivePositions.Any())
                {
                    positions = this.interpolator.Interpolate(
                        positions,
                        currentDrivePositions);
                }

                driver.Execute(positions);

                Thread.Sleep(10);
            }

            Thread.Sleep(150);
        }
    }
}