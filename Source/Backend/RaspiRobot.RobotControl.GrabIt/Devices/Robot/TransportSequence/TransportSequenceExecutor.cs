namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Settings;

internal class TransportSequenceExecutor
{
    private readonly TransportSequenceStepInterpolator interpolator;

    public TransportSequenceExecutor(TransportSequenceStepInterpolator interpolator)
    {
        this.interpolator = interpolator;
    }

    public async Task ExecuteAsync(IReadOnlyList<Sequence> sequences, IGrabItDriver driver)
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

                await Task.Delay(10);
            }

            await Task.Delay(150);
        }
    }
}