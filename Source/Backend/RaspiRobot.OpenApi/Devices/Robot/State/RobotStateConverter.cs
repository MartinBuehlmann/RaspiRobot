namespace RaspiRobot.OpenApi.Devices.Robot.State;

using System;
using Erowa.OpenAPI.Robot;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotStateConverter
{
    public RetrieveStateChangedResponse Convert(RobotState robotState)
        => robotState switch
        {
            RobotState.Ready => CreateReadyResponse(),
            RobotState.Disconnected => CreateNotReadyResponse(),
            RobotState.NotReady => CreateNotReadyResponse(),
            RobotState.Busy => CreateNotReadyResponse(),
            RobotState.Error => CreateNotReadyResponse(),
            _ => throw new ArgumentOutOfRangeException(nameof(robotState), robotState, "Unknown robot state"),
        };

    private static RetrieveStateChangedResponse CreateReadyResponse()
        => new RetrieveStateChangedResponse { Ready = new Empty() };

    private static RetrieveStateChangedResponse CreateNotReadyResponse()
        => new RetrieveStateChangedResponse { NotReady = new Empty() };
}