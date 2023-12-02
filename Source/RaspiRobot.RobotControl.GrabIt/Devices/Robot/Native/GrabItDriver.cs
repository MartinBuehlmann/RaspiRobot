namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using System;
using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

public class GrabItDriver : IGrabItDriver, IDisposable
{
    private readonly Pca9685Driver driver;

    public GrabItDriver(Pca9685Driver driver)
    {
        this.driver = driver;
    }

    public void Initialize()
    {
        this.driver.Initialize();
    }

    public void Execute(IReadOnlyList<Sequence> sequences)
    {
        // TODO: Implement sequence execution
    }

    public void Dispose()
    {
        this.driver?.Dispose();
    }
}