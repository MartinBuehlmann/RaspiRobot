namespace RaspiRobot.Web;

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

[ApiExplorerSettings(GroupName = WebConstants.Route)]
[Route($"{WebConstants.Route}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class WebController : Controller;