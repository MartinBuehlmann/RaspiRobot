namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Mdi;

using System;
using Common.Logging;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.OperationMode;
using RaspiRobot.RobotControl.Devices.Robot.Steps;

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

    public Position? Step(Axis axis, AxisDirection direction, int stepSize)
    {
        if (!this.IsInMdiMode)
        {
            this.LogNotInMdiMode(nameof(this.Step));
            return null;
        }

        var drive = (byte)axis;
        int currentValue = this.grabItDriver.CurrentDrivePositions[drive];
        int newValue = CalculateStepPosition(currentValue, direction, stepSize);

        if (currentValue != newValue)
        {
            var position = new Position(drive, newValue);
            this.grabItDriver.Execute(new[] { position });
            return position;
        }

        return null;
    }

    private static int CalculateStepPosition(int currentValue, AxisDirection direction, int stepSize)
    {
        return direction switch
        {
            AxisDirection.Minus => currentValue - stepSize,
            AxisDirection.Plus => currentValue + stepSize,
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