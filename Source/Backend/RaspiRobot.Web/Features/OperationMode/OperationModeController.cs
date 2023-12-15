namespace RaspiRobot.Web.Features.OperationMode;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl.OperationMode;

public class OperationModeController : WebController
{
    private readonly IOperationModeSetter operationModeSetter;
    private readonly IOperationModeRetriever operationModeRetriever;

    public OperationModeController(
        IOperationModeSetter operationModeSetter,
        IOperationModeRetriever operationModeRetriever)
    {
        this.operationModeSetter = operationModeSetter;
        this.operationModeRetriever = operationModeRetriever;
    }

    [HttpPut("{operationMode}")]
    public IActionResult SetOperationMode(OperationMode operationMode)
    {
        this.operationModeSetter.SetOperationMode(operationMode);
        return this.Ok();
    }

    [HttpGet("All")]
    public List<OperationModeInfo> GetOperationModes()
    {
        return Enum.GetValues<OperationMode>()
            .Select(x => new OperationModeInfo(x.ToString(), x))
            .ToList();
    }

    [HttpGet("Current")]
    public OperationMode GetOperationMode()
    {
        return this.operationModeRetriever.OperationMode;
    }
}