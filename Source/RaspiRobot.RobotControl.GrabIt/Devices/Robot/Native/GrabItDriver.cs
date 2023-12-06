namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using System;
using System.Collections.Generic;
using System.Threading;
using RaspiRobot.RobotControl.GrabIt.Settings;
using RaspiRobot.RobotControl.Settings;

public class GrabItDriver : IGrabItDriver, IDisposable
{
    private readonly Pca9685Driver driver;
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public GrabItDriver(Pca9685Driver driver)
    {
        this.driver = driver;
    }

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
        this.driver.Initialize();
    }

    public void Execute(IReadOnlyList<Sequence> sequences)
    {
        foreach (Sequence sequence in sequences)
        {
            this.Execute(sequence);
        }
    }

    public void Dispose()
    {
        this.driver?.Dispose();
    }

    private void Execute(Sequence sequence)
    {
        foreach (Step step in sequence.Steps)
        {
            this.Execute(step);
        }
    }

    private void Execute(Step step)
    {
        foreach (IPosition position in step.Positions)
        {
            var grabItPosition = (GrabItPosition)position;
            this.driver.SetPwm(grabItPosition.Drive, 0, grabItPosition.Value);
            this.currentDrivePositions[grabItPosition.Drive] = grabItPosition.Value;
            Thread.Sleep(50);
        }

        Thread.Sleep(750);
    }
}