namespace RaspiRobot.OpenApi.Devices.Storages.Magazine.State;

using System;
using Erowa.OpenAPI.Storage;
using RaspiRobot.RobotControl.Devices.Storages;

internal class MagazineStateConverter
{
    public MagazineState Convert(State state)
    {
        return state switch
        {
            State.Disconnected => MagazineState.Disconnected,
            State.NotReady => MagazineState.NotReady,
            State.Ready => MagazineState.Ready,
            State.Error => MagazineState.Error,
            _ => throw new NotSupportedException($"Invalid magazine state '{state}' detected."),
        };
    }
}