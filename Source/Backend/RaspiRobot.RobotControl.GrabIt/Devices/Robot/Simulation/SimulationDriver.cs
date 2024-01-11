namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Simulation;

using System.Collections.Generic;
using Common.Logging;
using EventBroker;
using RaspiRobot.RobotControl.Devices.Robot.AxisPosition;
using RaspiRobot.RobotControl.Settings;

internal class SimulationDriver : IGrabItDriver
{
    private readonly IEventBroker eventBroker;
    private readonly Log log;
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public SimulationDriver(
        IEventBroker eventBroker,
        Log log)
    {
        this.log = log;
        this.eventBroker = eventBroker;
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
            this.eventBroker.Publish(new RobotAxisPositionChangedEvent());
        }
    }
}