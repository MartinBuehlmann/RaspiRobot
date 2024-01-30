namespace RaspiRobot.Web.Features.Devices.Storages.AutoLinkMagazines;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Settings;

public class AutoLinkMagazinesController : WebController
{
    private readonly ISettingsRetriever settingsRetriever;

    public AutoLinkMagazinesController(ISettingsRetriever settingsRetriever)
    {
        this.settingsRetriever = settingsRetriever;
    }

    [HttpGet]
    public async Task<AutoLinkMagazineSelectionInfo[]> RetrieveMagazineSelectionsAsync()
    {
        IReadOnlyList<AutoLinkMagazineSettings> settings = await this.settingsRetriever.RetrieveAutoLinkMagazineSettingsAsync();
        return settings.Select(x => new AutoLinkMagazineSelectionInfo(x.Number, x.Name)).ToArray();
    }

    [HttpGet("{number:int}")]
    public async Task<AutoLinkMagazineInfo> RetrieveMagazineAsync(int number)
    {
        IReadOnlyList<AutoLinkMagazineSettings> settings = await this.settingsRetriever.RetrieveAutoLinkMagazineSettingsAsync();
        AutoLinkMagazineSettings? autoLinkMagazineSettings = settings.SingleOrDefault(x => x.Number == number);
        if (autoLinkMagazineSettings is not null)
        {
            return new AutoLinkMagazineInfo(
                autoLinkMagazineSettings.Number,
                autoLinkMagazineSettings.Name,
                autoLinkMagazineSettings.Places
                    .Select(x => new AutoLinkMagazinePlaceInfo(x.Number))
                    .ToArray());
        }

        throw new ResourceNotFoundException($"No autolink magazine found with number {number}.");
    }
}