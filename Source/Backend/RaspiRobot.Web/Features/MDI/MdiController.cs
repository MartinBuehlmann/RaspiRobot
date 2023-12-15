namespace RaspiRobot.Web.Features.MDI;

using RaspiRobot.RobotControl.OperationMode;
using RaspiRobot.Web.Features.OperationMode.Filters;

[RequiresOperationMode(OperationMode.Mdi)]
public abstract class MdiController : WebController;