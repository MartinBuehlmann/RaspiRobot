namespace RaspiRobot.Web;

using Microsoft.AspNetCore.Mvc;

[ApiExplorerSettings(GroupName = WebConstants.Route)]
[Route(WebConstants.Route + "/[controller]")]
[Consumes("application/json")]
public abstract class WebController : Controller
{
}