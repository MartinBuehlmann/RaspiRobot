namespace RaspiRobot.OpenApi.Devices.Storages.Magazine.State;

using System;
using Erowa.OpenAPI.Storage;

public class MagazineStateConverter
{
    public MagazineState Convert(RobotControl.Devices.Magazine.State state)
    {
        return state switch
        {
            RobotControl.Devices.Magazine.State.Disconnected => MagazineState.Disconnected,
            RobotControl.Devices.Magazine.State.NotReady => MagazineState.NotReady,
            RobotControl.Devices.Magazine.State.Ready => MagazineState.Ready,
            RobotControl.Devices.Magazine.State.Error => MagazineState.Error,
            _ => throw new NotSupportedException($"Invalid magazine state '{state}' detected."),
        };
    }
}