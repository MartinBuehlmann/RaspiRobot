namespace Common.Filters;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ResourceExceptionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context is { Exception: not null, ExceptionHandled: false })
        {
            ResolveException(context, context.Exception);
        }
    }

    private static void ResolveException(ActionExecutedContext context, Exception exception)
    {
        switch (exception)
        {
            case ResourceNotFoundException resourceNotFoundException:
                context.Result = new NotFoundObjectResult(resourceNotFoundException.Message);
                context.ExceptionHandled = true;
                break;
        }
    }
}