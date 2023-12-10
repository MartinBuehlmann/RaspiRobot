namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using System;
using System.Collections.Generic;
using RaspiRobot.RobotControl.GrabIt.Settings;

internal class SimulationDriver : IGrabItDriver
{
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
    }

    public void Execute(IReadOnlyList<GrabItPosition> positions)
    {
        foreach (GrabItPosition position in positions)
        {
            Console.WriteLine($"Setting drive {position.Drive} to value {position.Value}.");
            this.currentDrivePositions[position.Drive] = position.Value;
        }
    }
}