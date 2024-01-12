namespace RaspiRobot.Web.Features.Devices.Robot;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;

public class RobotController : WebController
{
    private readonly IDeviceService deviceService;

    public RobotController(IDeviceService deviceService)
    {
        this.deviceService = deviceService;
    }

    [HttpGet("Axis/All/CurrentPosition")]
    public List<PositionInfo> RetrieveCurrentPosition()
    {
        return this.deviceService
            .RetrieveRobot()
            .RetrieveAxisPositions()
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .ToList();
    }

    [HttpGet("Axis/{axis:int}/CurrentPosition")]
    public PositionInfo RetrieveCurrentPosition([Range(0, 5)] int axis)
    {
        return this.deviceService
            .RetrieveRobot()
            .RetrieveAxisPositions()
            .Where(x => x.Drive == axis)
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .Single();
    }
}