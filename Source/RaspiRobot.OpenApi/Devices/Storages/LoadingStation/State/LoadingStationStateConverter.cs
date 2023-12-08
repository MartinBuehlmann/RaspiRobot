namespace RaspiRobot.OpenApi.Devices.Storages.LoadingStation.State;

using System;
using Erowa.OpenAPI.Storage;

public class LoadingStationStateConverter
{
    public LoadingStationState Convert(RobotControl.Devices.Magazine.State state)
    {
        return state switch
        {
            RobotControl.Devices.Magazine.State.Disconnected => LoadingStationState.Disconnected,
            RobotControl.Devices.Magazine.State.NotReady => LoadingStationState.NotReady,
            RobotControl.Devices.Magazine.State.Ready => LoadingStationState.Ready,
            RobotControl.Devices.Magazine.State.Error => LoadingStationState.Error,
            _ => throw new NotSupportedException($"Invalid loading station state '{state}' detected."),
        };
    }
}