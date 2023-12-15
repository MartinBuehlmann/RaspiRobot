namespace RaspiRobot.RobotControl.OperationMode;

using System;

public interface IOperationModeRetriever
{
    event EventHandler OperationModeChanged;

    OperationMode OperationMode { get; }
}