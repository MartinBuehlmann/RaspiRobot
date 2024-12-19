namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using EventBroker;
using RaspiRobot.RobotControl.Devices.Robot.AxisPosition;
using RaspiRobot.RobotControl.Devices.Robot.Steps;
using RaspiRobot.RobotControl.GrabIt.Driver;
using RaspiRobot.RobotControl.Settings;

internal class GrabItDriver : IGrabItDriver, IDisposable
{
    private readonly Pca9685Driver driver;
    private readonly IEventBroker eventBroker;
    private readonly Log log;
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public GrabItDriver(
        Pca9685Driver driver,
        IEventBroker eventBroker,
        Log log)
    {
        this.driver = driver;
        this.log = log;
        this.eventBroker = eventBroker;
    }

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
        this.driver.Initialize();
        this.driver.SetPwmFrequency(50);
    }

    public async Task ExecuteAsync(IReadOnlyList<Position> positions)
    {
        foreach (Position position in positions)
        {
            if (!this.currentDrivePositions.ContainsKey(position.Drive) || this.currentDrivePositions[position.Drive] != position.Drive)
            {
                this.log.Verbose("Moving drive '{Drive}' to value '{Value}'", position.Drive, position.Value);
                this.driver.SetPwm(position.Drive, 0, position.Value);
                this.currentDrivePositions[position.Drive] = position.Value;
                this.eventBroker.Publish(new RobotAxisPositionChangedEvent());

                if (position.Drive != 0)
                {
                    await Task.Delay(5);
                }
            }
        }
    }

    public void Dispose()
    {
        this.driver.Dispose();
    }
}