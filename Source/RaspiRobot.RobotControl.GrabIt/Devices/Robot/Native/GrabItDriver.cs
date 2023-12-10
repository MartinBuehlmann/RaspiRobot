namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using System;
using System.Collections.Generic;
using System.Threading;
using RaspiRobot.Common.Logging;
using RaspiRobot.RobotControl.GrabIt.Settings;

internal class GrabItDriver : IGrabItDriver, IDisposable
{
    private readonly Pca9685Driver driver;
    private readonly Log log;
    private readonly Dictionary<byte, int> currentDrivePositions = new();

    public GrabItDriver(
        Pca9685Driver driver,
        Log log)
    {
        this.driver = driver;
        this.log = log;
    }

    public IReadOnlyDictionary<byte, int> CurrentDrivePositions => this.currentDrivePositions;

    public void Initialize()
    {
        this.driver.Initialize();
        this.driver.SetPwmFrequency(50);
    }

    public void Execute(IReadOnlyList<GrabItPosition> positions)
    {
        foreach (GrabItPosition position in positions)
        {
            if (!this.currentDrivePositions.ContainsKey(position.Drive) || this.currentDrivePositions[position.Drive] != position.Drive)
            {
                this.log.Verbose("Moving drive '{Drive}' to value '{Value}'", position.Drive, position.Value);
                this.driver.SetPwm(position.Drive, 0, position.Value);
                this.currentDrivePositions[position.Drive] = position.Value;

                if (position.Drive != 0)
                {
                    Thread.Sleep(5);
                }
            }
        }
    }

    public void Dispose()
    {
        this.driver?.Dispose();
    }
}