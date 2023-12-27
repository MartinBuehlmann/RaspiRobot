namespace RaspiRobot.Web.Features.MDI.Robot;

using System.ComponentModel.DataAnnotations;
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
}