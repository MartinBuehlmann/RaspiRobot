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
        return settings.Select(x => new AutoLinkMagazineSelectionInfo(x.Identifier, x.Name)).ToArray();
    }

    [HttpGet("{identifier:int}")]
    public async Task<AutoLinkMagazineInfo> RetrieveMagazineAsync(string identifier)
    {
        IReadOnlyList<AutoLinkMagazineSettings> settings = await this.settingsRetriever.RetrieveAutoLinkMagazineSettingsAsync();
        AutoLinkMagazineSettings? autoLinkMagazineSettings = settings.SingleOrDefault(x => x.Identifier == identifier);
        if (autoLinkMagazineSettings is not null)
        {
            return new AutoLinkMagazineInfo(
                autoLinkMagazineSettings.Identifier,
                autoLinkMagazineSettings.Name,
                autoLinkMagazineSettings.Places
                    .Select(x => new AutoLinkMagazinePlaceInfo(x.Identifier))
                    .ToArray());
        }

        throw new ResourceNotFoundException($"No autolink magazine found with number {identifier}.");
    }
}