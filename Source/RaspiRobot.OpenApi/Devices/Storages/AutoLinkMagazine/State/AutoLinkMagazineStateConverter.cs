namespace RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine.State;

using System;
using Erowa.OpenAPI.Storage;

public class AutoLinkMagazineStateConverter
{
    public AutoLinkMagazineState Convert(RobotControl.Devices.Magazine.State state)
    {
        return state switch
        {
            RobotControl.Devices.Magazine.State.Disconnected => AutoLinkMagazineState.Disconnected,
            RobotControl.Devices.Magazine.State.NotReady => AutoLinkMagazineState.NotReady,
            RobotControl.Devices.Magazine.State.Ready => AutoLinkMagazineState.Ready,
            RobotControl.Devices.Magazine.State.Error => AutoLinkMagazineState.Error,
            _ => throw new NotSupportedException($"Invalid magazine state '{state}' detected."),
        };
    }
}