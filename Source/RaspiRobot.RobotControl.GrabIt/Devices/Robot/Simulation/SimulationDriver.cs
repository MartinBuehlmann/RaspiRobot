namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using System;
using System.Collections.Generic;
using System.Threading;
using RaspiRobot.RobotControl.GrabIt.Settings;
using RaspiRobot.RobotControl.Settings;

public class SimulationDriver : IGrabItDriver
{
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
    }

    public void Execute(IReadOnlyList<Sequence> sequences)
    {
        foreach (Sequence sequence in sequences)
        {
            this.Execute(sequence);
        }
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
            Console.WriteLine($"Setting drive {grabItPosition.Drive} to value {grabItPosition.Value}.");
            this.currentDrivePositions[grabItPosition.Drive] = grabItPosition.Value;
        }

        Thread.Sleep(500);
    }
}