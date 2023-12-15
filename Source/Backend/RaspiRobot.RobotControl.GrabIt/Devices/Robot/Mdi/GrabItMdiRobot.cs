namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Mdi;

using System;
using RaspiRobot.Common.Logging;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.GrabIt.Settings;
using RaspiRobot.RobotControl.OperationMode;

internal class GrabItMdiRobot : IMdiRobot
{
    private readonly IOperationModeRetriever operationModeRetriever;
    private readonly IGrabItDriver grabItDriver;
    private readonly Log log;

    public GrabItMdiRobot(
        IOperationModeRetriever operationModeRetriever,
        IGrabItDriver grabItDriver,
        Log log)
    {
        this.operationModeRetriever = operationModeRetriever;
        this.grabItDriver = grabItDriver;
        this.log = log;
    }

    private bool IsInMdiMode
        => this.operationModeRetriever.OperationMode == OperationMode.Mdi;

    public bool Step(Axis axis, AxisDirection direction)
    {
        if (!this.IsInMdiMode)
        {
            this.LogNotInMdiMode(nameof(this.Step));
            return false;
        }

        var drive = (byte)axis;
        int currentValue = this.grabItDriver.CurrentDrivePositions[drive];
        int newValue = CalculateStepPosition(currentValue, direction);

        if (currentValue != newValue)
        {
            this.grabItDriver.Execute(new[] { new GrabItPosition(drive, newValue) });
            return true;
        }

        return false;
    }

    private static int CalculateStepPosition(int currentValue, AxisDirection direction)
    {
        return direction switch
        {
            AxisDirection.Minus => currentValue - 1,
            AxisDirection.Plus => currentValue + 1,
            _ => throw new InvalidOperationException($"Unknown axis direction '{direction}'."),
        };
    }

    private void LogNotInMdiMode(string mdiFunctionName)
    {
        this.log.Warn(
            "Unable to execute MDI function {MdiFunctionName} (Robot is not in MDI mode)",
            mdiFunctionName);
    }
}