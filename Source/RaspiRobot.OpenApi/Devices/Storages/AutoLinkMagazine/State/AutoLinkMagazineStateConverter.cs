namespace RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine.State;

using System;
using Erowa.OpenAPI.Storage;
using RaspiRobot.RobotControl.Devices.Storages;

public class AutoLinkMagazineStateConverter
{
    public AutoLinkMagazineState Convert(State state)
    {
        return state switch
        {
            State.Disconnected => AutoLinkMagazineState.Disconnected,
            State.NotReady => AutoLinkMagazineState.NotReady,
            State.Ready => AutoLinkMagazineState.Ready,
            State.Error => AutoLinkMagazineState.Error,
            _ => throw new NotSupportedException($"Invalid magazine state '{state}' detected."),
        };
    }
}