namespace RaspiRobot.Web.Features.MDI.Robot;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot.Mdi;
using RaspiRobot.RobotControl.Devices.Robot.Steps;
using Swashbuckle.AspNetCore.Annotations;

[Route($"{WebConstants.Route}/Mdi/[controller]")]
public class RobotController : MdiController
{
    private readonly IMdiRobot mdiRobot;

    public RobotController(IDeviceService deviceService)
    {
        this.mdiRobot = deviceService.RetrieveRobot().MdiRobot;
    }

    [SwaggerOperation(Tags = [SwaggerTagConstants.Mdi])]
    [HttpPut("Axis/{axis}/Step/{direction}/{stepSize}")]
#pragma warning disable VSTHRD200
    public async Task<SteppingResultInfo> Step(
        [Range(0, 5)] int axis,
        AxisDirection direction,
        [Range(1, 10)] int stepSize)
    {
        var axisValue = (Axis)axis;
        Position? newPosition = await this.mdiRobot.StepAsync(axisValue, direction, stepSize);
        PositionInfo? newPositionInfo =
            newPosition is not null ? new PositionInfo(newPosition.Drive, newPosition.Value) : null;
        return new SteppingResultInfo(newPosition is not null, newPositionInfo);
    }
#pragma warning restore VSTHRD200
}