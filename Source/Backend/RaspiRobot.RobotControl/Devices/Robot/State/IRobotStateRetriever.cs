namespace RaspiRobot.RobotControl.Devices.Robot.State;

using System;

public interface IRobotStateRetriever
{
    event Action? StateChangedChanged;

    RobotState RobotState { get; }
}