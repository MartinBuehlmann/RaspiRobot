namespace RaspiRobot.OpenApi.Devices.Robot.Busy;

using System;
using Erowa.OpenAPI.Robot.Busy;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl.Devices.Robot.State;

internal class RobotBusyStateConverter
{
    public RetrieveBusyStateChangedResponse Convert(RobotState robotState)
        => robotState switch
        {
            RobotState.Ready => CreateNotBusyResponse(),
            RobotState.Disconnected => CreateNotBusyResponse(),
            RobotState.NotReady => CreateNotBusyResponse(),
            RobotState.Busy => CreateBusyResponse(),
            RobotState.Error => CreateNotBusyResponse(),
            _ => throw new ArgumentOutOfRangeException(nameof(robotState), robotState, "Unknown robot state"),
        };

    private static RetrieveBusyStateChangedResponse CreateBusyResponse()
        => new RetrieveBusyStateChangedResponse { Busy = new Empty() };

    private static RetrieveBusyStateChangedResponse CreateNotBusyResponse()
        => new RetrieveBusyStateChangedResponse { NotBusy = new Empty() };
}