namespace RaspiRobot.OpenApi.Devices.Robot.ChuckLoading;

using System;
using Erowa.OpenAPI.Robot;

internal class ChuckLoadingConverter
{
    public ChuckLoading Convert(RobotControl.Devices.Robot.ChuckLoading.ChuckLoading chuckLoading)
        => chuckLoading.Loading switch
        {
            RobotControl.Devices.Robot.ChuckLoading.EmptyChuckLoading => new ChuckLoading
            {
                Chuck = new Chuck { Number = chuckLoading.Chuck.Number },
                Empty = new EmptyChuckLoading(),
            },
            RobotControl.Devices.Robot.ChuckLoading.PalletChuckLoading palletChuckLoading => new ChuckLoading
            {
                Chuck = new Chuck { Number = chuckLoading.Chuck.Number },
                Pallet = new PalletChuckLoading
                {
                    PalletOnChuckHomePlace = new Place { Number = palletChuckLoading.Place.Number },
                    TagId = palletChuckLoading.TagId,
                },
            },
            _ => throw new ArgumentOutOfRangeException($"Chuck loading of type '{chuckLoading.Loading}' is not supported."),
        };
}