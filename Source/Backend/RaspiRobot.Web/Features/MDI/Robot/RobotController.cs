namespace RaspiRobot.Web.Features.MDI.Robot;

using System.Threading.Tasks;
using RaspiRobot.RobotControl;

public class RobotController : MdiController
{
    private readonly IDeviceService deviceService;

    public RobotController(IDeviceService deviceService)
    {
        this.deviceService = deviceService;
    }

    public Task MoveToTransferPositionAsync()
    {
        return Task.CompletedTask;
    }
}