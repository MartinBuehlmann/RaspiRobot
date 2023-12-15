namespace RaspiRobot.Web.Features.OperationMode.Filters;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.RobotControl.OperationMode;

public class RequiresOperationModeAttribute : ActionFilterAttribute
{
    private readonly OperationMode operationMode;

    public RequiresOperationModeAttribute(OperationMode operationMode)
    {
        this.operationMode = operationMode;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        OperationMode currentOperationMode = context
            .HttpContext
            .RequestServices.GetService<IOperationModeRetriever>()!
            .OperationMode;

        if (currentOperationMode != this.operationMode)
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
        }
    }
}