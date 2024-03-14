namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.TransportSequence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBroker;
using RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;
using RaspiRobot.RobotControl.Devices.Robot.Steps;

internal class TransportSequenceExecutor
{
    private readonly TransportSequenceStepInterpolator interpolator;
    private readonly IEventBroker eventBroker;

    public TransportSequenceExecutor(
        TransportSequenceStepInterpolator interpolator,
        IEventBroker eventBroker)
    {
        this.interpolator = interpolator;
        this.eventBroker = eventBroker;
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
        this.eventBroker.Publish(new ChuckLoadingChangedEvent(new[] { chuckLoadingChangedNotificationStep.ChuckLoading }));
        return Task.CompletedTask;
    }
}