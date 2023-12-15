namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System;
using Erowa.OpenAPI.Robot;

internal class RobotStateConverter
{
    public RobotState Convert(RobotControl.Devices.Robot.State state)
    {
        return state switch
        {
            RobotControl.Devices.Robot.State.Disconnected => RobotState.Disconnected,
            RobotControl.Devices.Robot.State.NotReady => RobotState.NotReady,
            RobotControl.Devices.Robot.State.Ready => RobotState.Ready,
            RobotControl.Devices.Robot.State.Busy => RobotState.Busy,
            RobotControl.Devices.Robot.State.Error => RobotState.Error,
            _ => throw new NotSupportedException($"Invalid robot state '{state}' detected."),
        };
    }
}