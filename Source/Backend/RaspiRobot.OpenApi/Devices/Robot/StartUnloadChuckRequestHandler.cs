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

    public async Task<CommandResponse> HandleUnloadChuckAsync(StartUnloadChuck request)
    {
        this.logger.Info(
            "Received request to unload chuck: '{ChuckNumber}' to place: '{PlaceNumber}'",
            request.Chuck.Number,
            request.PlaceToUnload.Number);

        IRobot robot = this.deviceService.RetrieveRobot();

        this.logger.Info(
            "Start unload to chuck: '{ChuckNumber}' to place: '{PlaceNumber}'",
            request.Chuck,
            request.PlaceToUnload);

        ICommandResponse response = await robot.UnloadChuckAsync(
            new MachineChuck(request.Chuck.Number),
            new StoragePlace(request.PlaceToUnload.Number));

        this.logger.Info("Unload chuck ended with result: {Response}", response);

        return response.ToCommandResponse();
    }
}