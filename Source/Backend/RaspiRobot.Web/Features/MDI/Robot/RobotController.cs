namespace RaspiRobot.Web.Features.MDI.Robot;

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

    // TODO: IPosition is not serialized as it should.
    [HttpPut("Step/{axis}/{direction}")]
    public StepResponseInfo Step(Axis axis, AxisDirection direction)
    {
        IPosition? newPosition = this.mdiRobot.Step(axis, direction);
        return new StepResponseInfo(newPosition is not null, newPosition);
    }
}