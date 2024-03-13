namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Robot.Steps;
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
            foreach (IStep step in sequence.Steps)
            {
                await this.ExecuteAsync(step, driver);
            }

            await Task.Delay(150);
        }
    }

    private async Task ExecuteAsync(IStep step, IGrabItDriver driver)
    {
        switch (step)
        {
            case MoveStep moveStep:
                await this.ExecuteAsync(moveStep, driver);
                break;

            case ChuckLoadingChangedNotificationStep chuckLoadingChangedNotificationStep:
                await this.ExecuteAsync(chuckLoadingChangedNotificationStep);
                break;

            default:
                throw new NotSupportedException($"Execution of step of type '{step.GetType()}' is not supported.");
        }
    }

    private async Task ExecuteAsync(MoveStep moveStep, IGrabItDriver driver)
    {
        IReadOnlyDictionary<byte, int> currentDrivePositions = driver.CurrentDrivePositions;
        IReadOnlyList<Position> positions = moveStep.Positions;

        if (currentDrivePositions.Any())
        {
            positions = this.interpolator.Interpolate(
                positions,
                currentDrivePositions);
        }

        driver.Execute(positions);

        await Task.Delay(10);
    }

    private Task ExecuteAsync(ChuckLoadingChangedNotificationStep chuckLoadingChangedNotificationStep)
    {
        // TODO: Implement chuck loading changed notification.
        return Task.CompletedTask;
    }
}