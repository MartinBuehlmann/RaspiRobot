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
    public IActionResult SetOperationMode(string operationMode)
    {
        OperationMode newOperationMode = Enum.Parse<OperationMode>(operationMode);
        this.operationModeSetter.SetOperationMode(newOperationMode);
        return this.Ok();
    }

    [HttpGet("All")]
    public List<string> GetOperationModes()
    {
        return Enum.GetValues<OperationMode>()
            .Select(x => x.ToString())
            .ToList();
    }

    [HttpGet("Current")]
    public string GetOperationMode()
    {
        return this.operationModeRetriever
            .OperationMode
            .ToString();
    }
}