namespace RaspiRobot.OpenApi.Devices.Robot;

using System.Threading.Tasks;
using Common.Logging;
using Erowa.OpenAPI.Robot;
using RaspiRobot.OpenApi.Devices.Shared;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Commands;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot;
using RaspiRobot.RobotControl.Devices.Storages;

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

    public async Task<CommandResponse> HandleLoadChuckAsync(StartLoadChuck request)
    {
        this.logger.Info(
            "Received request to load chuck: '{ChuckNumber}' from place: '{PlaceNumber}'",
            request.Chuck.Number,
            request.PlaceToLoad.Number);

        IRobot robot = this.deviceService.RetrieveRobot();

        StoragePlace? destinationPlaceForPalletOnChuck = request.PlaceToUnloadPalletOnChuck is not null
            ? new StoragePlace(request.PlaceToUnloadPalletOnChuck.Number)
            : null;

        this.logger.Info(
            "Start load to chuck: '{ChuckNumber}' from place: '{PlaceNumber}'",
            request.Chuck,
            request.PlaceToLoad);

        ICommandResponse response = await robot.LoadChuckAsync(
            new StoragePlace(request.PlaceToLoad.Number),
            new MachineChuck(request.Chuck.Number),
            destinationPlaceForPalletOnChuck);

        this.logger.Info("Load chuck ended with result: {Response}", response);

        return response.ToCommandResponse();
    }
}