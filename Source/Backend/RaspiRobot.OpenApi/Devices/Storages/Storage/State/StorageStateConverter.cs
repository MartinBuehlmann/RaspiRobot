namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using System;
using Erowa.OpenAPI.Storage;
using RaspiRobot.RobotControl.Devices.Storages;

internal class StorageStateConverter
{
    public StorageState Convert(State state)
    {
        return state switch
        {
            State.Disconnected => StorageState.Disconnected,
            State.NotReady => StorageState.NotReady,
            State.Ready => StorageState.Ready,
            State.Error => StorageState.Error,
            _ => throw new NotSupportedException($"Invalid storage state '{state}' detected."),
        };
    }
}