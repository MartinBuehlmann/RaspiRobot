﻿namespace RaspiRobot.RobotControl.Devices.Robot;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.State;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Settings;

public interface IRobot : IDevice
{
    IMdiRobot MdiRobot { get; }

    IReadOnlyList<PositionSettings> RetrieveAxisPositions();

    Task SubscribeForStateChangedAsync(
        IRobotStateNotifier robotStateNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForAlarmsChangedAsync(
        IAlarmsNotifier alarmsNotifier,
        CancellationToken cancellationToken);

    Task SubscribeForChuckLoadingsChangedAsync(
        int[] chuckNumbers,
        IChuckOccupancyNotifier chuckOccupancyNotifier,
        CancellationToken cancellationToken);

    Task<ICommandResponse> LoadChuckAsync(
        StoragePlace sourcePlace,
        MachineChuck chuck,
        StoragePlace? destinationPlaceForPalletOnChuck,
        CancellationToken rollbackCancellationToken);

    Task<ICommandResponse> UnloadChuckAsync(
        MachineChuck chuck,
        StoragePlace destinationPlace,
        CancellationToken rollbackCancellationToken);

    Task<ICommandResponse> ExchangeStoragePlaceAsync(
        StoragePlace sourcePlace,
        StoragePlace destinationPlace);
}