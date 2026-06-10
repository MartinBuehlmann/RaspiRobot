namespace RaspiRobot.Web.Features.Devices.Robot;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Alarms;
using RaspiRobot.Web.Features.Devices.Robot.Alarms;
using Severity = RaspiRobot.Web.Features.Devices.Robot.Alarms.Severity;

public class RobotController : WebController
{
    private readonly IDeviceService deviceService;

    public RobotController(IDeviceService deviceService)
    {
        this.deviceService = deviceService;
    }

    [HttpGet("Axis/All/CurrentPosition")]
    public List<PositionInfo> RetrieveCurrentPosition()
    {
        return this.deviceService
            .RetrieveRobot()
            .RetrieveAxisPositions()
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .ToList();
    }

    [HttpGet("Axis/{axis:int}/CurrentPosition")]
    public PositionInfo RetrieveCurrentPosition([Range(0, 5)] int axis)
    {
        return this.deviceService
            .RetrieveRobot()
            .RetrieveAxisPositions()
            .Where(x => x.Drive == axis)
            .Select(x => new PositionInfo(x.Drive, x.Value))
            .Single();
    }

    [HttpGet("Alarms")]
    public AlarmInfo[] RetrieveAlarms()
    {
        return this.deviceService
            .RetrieveRobot()
            .Alarms
            .RetrieveAlarms()
            .Select(x => new AlarmInfo(x.Code, x.Message, x.DateTime, ConvertToSeverity(x.Severity), x.IsActive))
            .ToArray();
    }

    [HttpPatch("Alarms/{code}/IsActive")]
    public IActionResult UpdateAlarmIsActive([Required] string code, [FromBody] bool isActive)
    {
        IAlarmsFacade alarmsFacade = this.deviceService
            .RetrieveRobot()
            .Alarms;

        if (!alarmsFacade.UpdateAlarmActivation(code, isActive))
        {
            throw new ResourceNotFoundException($"No alarm with code {code} found");
        }

        return this.Ok();
    }


    private static Severity ConvertToSeverity(RobotControl.Devices.Alarms.Severity severity)
    {
        return severity switch
        {
            RobotControl.Devices.Alarms.Severity.Information => Severity.Information,
            RobotControl.Devices.Alarms.Severity.Warning => Severity.Warning,
            RobotControl.Devices.Alarms.Severity.Error => Severity.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, "Invalid severity value"),
        };
    }
}