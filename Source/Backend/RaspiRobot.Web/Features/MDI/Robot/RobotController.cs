namespace RaspiRobot.Web.Features.MDI.Robot;

using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;

public class RobotController : MdiController
{
    private readonly IMdiRobot mdiRobot;

    public RobotController(IDeviceService deviceService)
    {
        this.mdiRobot = deviceService.RetrieveRobot().MdiRobot;
    }

    [HttpPut("Step/{axis}/{direction}")]
    public bool Step(Axis axis, AxisDirection direction)
    {
        return this.mdiRobot.Step(axis, direction);
    }
}