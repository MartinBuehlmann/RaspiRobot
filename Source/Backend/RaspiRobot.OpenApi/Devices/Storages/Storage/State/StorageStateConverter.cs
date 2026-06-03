namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using System;
using Erowa.OpenAPI.Storage.VisuLink;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl.Devices.Storages.State;

internal class StorageStateConverter
{
    public RetrieveStateChangedResponse Convert(State state)
    {
        return state switch
        {
            State.Disconnected => CreateNotReadyResponse(),
            State.NotReady => CreateNotReadyResponse(),
            State.Ready => CreateReadyResponse(),
            State.Error => CreateNotReadyResponse(),
            _ => throw new NotSupportedException($"Invalid storage state '{state}' detected."),
        };
    }

    private static RetrieveStateChangedResponse CreateReadyResponse()
        => new RetrieveStateChangedResponse { Ready = new Empty() };

    private static RetrieveStateChangedResponse CreateNotReadyResponse()
        => new RetrieveStateChangedResponse { NotReady = new Empty() };
}