namespace RaspiRobot.Web.Features.Devices.Storages.Magazines;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Settings;

public class MagazinesController : WebController
{
    private readonly ISettingsRetriever settingsRetriever;

    public MagazinesController(ISettingsRetriever settingsRetriever)
    {
        this.settingsRetriever = settingsRetriever;
    }

    [HttpGet]
    public async Task<MagazineSelectionInfo[]> RetrieveMagazineSelectionsAsync()
    {
        IReadOnlyList<MagazineSettings> settings = await this.settingsRetriever.RetrieveMagazineSettingsAsync();
        return settings.Select(x => new MagazineSelectionInfo(x.Identifier, x.Name)).ToArray();
    }

    [HttpGet("{identifier}")]
    public async Task<MagazineInfo> RetrieveMagazineAsync(string identifier)
    {
        IReadOnlyList<MagazineSettings> settings = await this.settingsRetriever.RetrieveMagazineSettingsAsync();
        MagazineSettings? magazineSettings = settings.SingleOrDefault(x => x.Identifier == identifier);

        if (magazineSettings is not null)
        {
            return new MagazineInfo(
                magazineSettings.Identifier,
                magazineSettings.Name,
                magazineSettings.Places
                    .Select(x => new MagazinePlaceInfo(x.Identifier))
                    .ToArray());
        }

        throw new ResourceNotFoundException($"No magazine found with number {identifier}.");
    }
}