namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotStateConverter
{
    public Erowa.OpenAPI.Robot.RobotState Convert(RobotState state)
    {
        return state switch
        {
            RobotState.Disconnected => Erowa.OpenAPI.Robot.RobotState.Disconnected,
            RobotState.NotReady => Erowa.OpenAPI.Robot.RobotState.NotReady,
            RobotState.Ready => Erowa.OpenAPI.Robot.RobotState.Ready,
            RobotState.Busy => Erowa.OpenAPI.Robot.RobotState.Busy,
            RobotState.Error => Erowa.OpenAPI.Robot.RobotState.Error,
            _ => throw new NotSupportedException($"Invalid robot state '{state}' detected."),
        };
    }
}