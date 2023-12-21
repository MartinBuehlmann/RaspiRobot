namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using System.Collections.Generic;
using Common.Logging;
using RaspiRobot.RobotControl.Settings;

internal class SimulationDriver : IGrabItDriver
{
    private readonly Log log;
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public SimulationDriver(Log log)
    {
        this.log = log;
    }

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
    }

    public void Execute(IReadOnlyList<Position> positions)
    {
        foreach (Position position in positions)
        {
            this.log.Debug("Setting drive {Drive} to value {Value}", position.Drive, position.Value);
            this.currentDrivePositions[position.Drive] = position.Value;
        }
    }
}