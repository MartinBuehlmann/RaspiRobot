namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using System;
using System.Globalization;
using Erowa.OpenAPI.MachineLoading;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using ChuckOccupancy = Erowa.OpenAPI.MachineLoading.ChuckOccupancy;
using StoragePlace = Erowa.OpenAPI.MachineLoading.StoragePlace;

internal class ChuckOccupancyConverter
{
    public ChuckOccupancy Convert(RobotControl.Devices.Robot.ChuckOccupancy.ChuckOccupancy chuckOccupancy)
        => chuckOccupancy.Occupancy switch
        {
            EmptyChuckOccupancy => new ChuckOccupancy
            {
                Chuck = new Chuck
                {
                    Identifier = chuckOccupancy.Chuck.Number.ToString(CultureInfo.InvariantCulture),
                },
                Empty = new Empty(),
            },
            PalletChuckOccupancy palletChuckOccupancy => new ChuckOccupancy
            {
                Chuck = new Chuck
                {
                    Identifier = chuckOccupancy.Chuck.Number.ToString(CultureInfo.InvariantCulture),
                },
                Pallet = new Pallet
                {
                    SourcePlace = new StoragePlace
                        { Identifier = palletChuckOccupancy.Place.Number.ToString(CultureInfo.InvariantCulture) },
                },
            },
            _ => throw new ArgumentOutOfRangeException(
                $"Chuck occupancy of type '{chuckOccupancy.Occupancy}' is not supported."),
        };
}