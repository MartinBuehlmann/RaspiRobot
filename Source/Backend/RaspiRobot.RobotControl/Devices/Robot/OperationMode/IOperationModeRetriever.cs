namespace RaspiRobot.RobotControl.Devices.Robot.OperationMode;

using System;

public interface IOperationModeRetriever
{
    event EventHandler OperationModeChanged;

    OperationMode OperationMode { get; }
}