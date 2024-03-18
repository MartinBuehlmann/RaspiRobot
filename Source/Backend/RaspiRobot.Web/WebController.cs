namespace RaspiRobot.Web;

using System.Net.Mime;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;

[ApiExplorerSettings(GroupName = WebConstants.Route)]
[Route($"{WebConstants.Route}/[controller]")]
[ResourceException]
[Produces(MediaTypeNames.Application.Json)]
public abstract class WebController : Controller;