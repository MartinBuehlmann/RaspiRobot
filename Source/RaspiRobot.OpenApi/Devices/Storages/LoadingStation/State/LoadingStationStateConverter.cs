namespace RaspiRobot.OpenApi.Devices.Storages.LoadingStation.State;

using System;
using Erowa.OpenAPI.Storage;
using RaspiRobot.RobotControl.Devices.Storages;

internal class LoadingStationStateConverter
{
    public LoadingStationState Convert(State state)
    {
        return state switch
        {
            State.Disconnected => LoadingStationState.Disconnected,
            State.NotReady => LoadingStationState.NotReady,
            State.Ready => LoadingStationState.Ready,
            State.Error => LoadingStationState.Error,
            _ => throw new NotSupportedException($"Invalid loading station state '{state}' detected."),
        };
    }
}