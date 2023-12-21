namespace RaspiRobot.Web.Features.MDI.Robot;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Settings;

public class RobotController : MdiController
{
    private readonly IMdiRobot mdiRobot;

    public RobotController(IDeviceService deviceService)
    {
        this.mdiRobot = deviceService.RetrieveRobot().MdiRobot;
    }

    [HttpPut("Step/{axis}/{direction}")]
    public StepResponseInfo Step(
        [Range(0, 5)] int axis,
        AxisDirection direction)
    {
        var axisValue = (Axis)axis;
        Position? newPosition = this.mdiRobot.Step(axisValue, direction);
        return new StepResponseInfo(newPosition is not null, newPosition);
    }
}