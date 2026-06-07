namespace RaspiRobot.OpenApi.Devices.Robot;

using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Erowa.OpenAPI.Robot;
using Google.Protobuf.WellKnownTypes;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot;
using StoragePlace = RaspiRobot.RobotControl.Devices.Storages.StoragePlace;

internal class StartLoadChuckRequestHandler
{
    private readonly IDeviceService deviceService;
    private readonly Log logger;

    public StartLoadChuckRequestHandler(
        IDeviceService deviceService,
        Log logger)
    {
        this.deviceService = deviceService;
        this.logger = logger;
    }

    public async Task<LoadChuckResponse> HandleLoadChuckAsync(
        StartLoadChuck request,
        CancellationToken rollbackCancellationToken)
    {
        this.logger.Info(
            "Received request to load chuck: '{ChuckNumber}' from place: '{PlaceNumber}'",
            request.Chuck.Identifier,
            request.PlaceToLoad.Identifier);

        IRobot robot = this.deviceService.RetrieveRobot();

        StoragePlace? destinationPlaceForPalletOnChuck = request.PlaceToUnloadPalletOnChuck is not null
            ? new StoragePlace(request.PlaceToUnloadPalletOnChuck.Identifier)
            : null;

        this.logger.Info(
            "Start load to chuck: '{ChuckNumber}' from place: '{PlaceNumber}'",
            request.Chuck,
            request.PlaceToLoad);

        ICommandResponse response = await robot.LoadChuckAsync(
            new StoragePlace(request.PlaceToLoad.Identifier),
            new MachineChuck(request.Chuck.Identifier),
            destinationPlaceForPalletOnChuck,
            rollbackCancellationToken);

        this.logger.Info("Load chuck ended with result: {Response}", response);

        return CreateResponse(response);
    }

    private static LoadChuckResponse CreateResponse(ICommandResponse response)
        => response switch
        {
            ErrorResponse errorResponse => new LoadChuckResponse
                { Unsuccessful = new Unsuccessful { Message = errorResponse.Message } },

            SuccessResponse => new LoadChuckResponse { Successful = new Empty() },

            _ => throw new ArgumentOutOfRangeException(nameof(response), response, "Unknown response type"),
        };
}