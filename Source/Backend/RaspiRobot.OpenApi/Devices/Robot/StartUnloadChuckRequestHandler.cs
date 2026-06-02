namespace RaspiRobot.OpenApi.Devices.Robot;

using System;
using System.Globalization;
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

internal class StartUnloadChuckRequestHandler
{
    private readonly IDeviceService deviceService;
    private readonly Log logger;

    public StartUnloadChuckRequestHandler(
        IDeviceService deviceService,
        Log logger)
    {
        this.deviceService = deviceService;
        this.logger = logger;
    }

    public async Task<UnloadChuckResponse> HandleUnloadChuckAsync(
        StartUnloadChuck request,
        CancellationToken rollbackCancellationToken)
    {
        this.logger.Info(
            "Received request to unload chuck: '{ChuckNumber}' to place: '{PlaceNumber}'",
            request.Chuck.Identifier,
            request.PlaceToUnload.Identifier);

        IRobot robot = this.deviceService.RetrieveRobot();

        this.logger.Info(
            "Start unload to chuck: '{ChuckNumber}' to place: '{PlaceNumber}'",
            request.Chuck,
            request.PlaceToUnload);

        ICommandResponse response = await robot.UnloadChuckAsync(
            new MachineChuck(int.Parse(request.Chuck.Identifier, CultureInfo.InvariantCulture)),
            new StoragePlace(int.Parse(request.PlaceToUnload.Identifier, CultureInfo.InvariantCulture)),
            rollbackCancellationToken);

        this.logger.Info("Unload chuck ended with result: {Response}", response);

        return CreateResponse(response);
    }

    private static UnloadChuckResponse CreateResponse(ICommandResponse response)
        => response switch
        {
            ErrorResponse errorResponse => new UnloadChuckResponse
                { Unsuccessful = new Unsuccessful { Message = errorResponse.Message } },

            SuccessResponse => new UnloadChuckResponse { Successful = new Empty() },

            _ => throw new ArgumentOutOfRangeException(nameof(response), response, "Unknown response type"),
        };
}