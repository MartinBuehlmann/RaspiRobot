namespace RaspiRobot.Web.Features.MDI.Robot;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Settings;

[Route($"{WebConstants.Route}/Mdi/[controller]")]
public class RobotController : MdiController
{
    private readonly IMdiRobot mdiRobot;

    public RobotController(IDeviceService deviceService)
    {
        this.mdiRobot = deviceService.RetrieveRobot().MdiRobot;
    }

    [HttpPut("Axis/{axis}/Step/{direction}")]
    public StepResponseInfo Step(
        [Range(0, 5)] int axis,
        AxisDirection direction)
    {
        var axisValue = (Axis)axis;
        Position? newPosition = this.mdiRobot.Step(axisValue, direction);
        PositionInfo? newPositionInfo =
            newPosition is not null ? new PositionInfo(newPosition.Drive, newPosition.Value) : null;
        return new StepResponseInfo(newPosition is not null, newPositionInfo);
    }

    [HttpGet("Axis/All/CurrentPosition")]
    public List<PositionInfo> RetrieveCurrentPosition()
    {
        return this.mdiRobot.RetrieveAxisPositions()
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .ToList();
    }

    [HttpGet("Axis/{axis}/CurrentPosition")]
    public PositionInfo RetrieveCurrentPosition([Range(0, 5)] int axis)
    {
        return this.mdiRobot.RetrieveAxisPositions()
            .Where(x => x.Drive == axis)
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .Single();
    }
}