namespace RaspiRobot.Web.Features.Devices;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class DeviceController : WebController
{
    private readonly DevicesRetriever devicesRetriever;

    public DeviceController(DevicesRetriever devicesRetriever)
    {
        this.devicesRetriever = devicesRetriever;
    }

    [HttpGet("All")]
    public async Task<Devices> RetrieveAllDevicesAsync()
    {
        return await this.devicesRetriever.RetrieveAllAsync();
    }
}