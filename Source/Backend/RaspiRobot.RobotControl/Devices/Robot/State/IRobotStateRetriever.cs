namespace RaspiRobot.RobotControl.Devices.Robot.State;

using System;

public interface IRobotStateRetriever
{
    event EventHandler StateChangedChanged;

    RobotState RobotState { get; }
}