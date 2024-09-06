namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using System;
using Erowa.OpenAPI;
using Erowa.OpenAPI.Robot;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using ChuckOccupancy = Erowa.OpenAPI.Robot.ChuckOccupancy;

internal class ChuckOccupancyConverter
{
    public ChuckOccupancy Convert(RobotControl.Devices.Robot.ChuckOccupancy.ChuckOccupancy chuckOccupancy)
        => chuckOccupancy.Occupancy switch
        {
            EmptyChuckOccupancy => new ChuckOccupancy
            {
                Chuck = new Chuck { Number = chuckOccupancy.Chuck.Number },
                Empty = new Empty(),
            },
            PalletChuckOccupancy palletChuckOccupancy => new ChuckOccupancy
            {
                Chuck = new Chuck { Number = chuckOccupancy.Chuck.Number },
                Pallet = new Pallet
                {
                    PalletOnChuckHomePlace = new StoragePlace { Number = palletChuckOccupancy.Place.Number },
                    TagId = palletChuckOccupancy.TagId,
                },
            },
            _ => throw new ArgumentOutOfRangeException($"Chuck occupancy of type '{chuckOccupancy.Occupancy}' is not supported."),
        };
}