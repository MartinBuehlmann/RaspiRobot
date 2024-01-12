namespace RaspiRobot.Web.Features.Devices.Magazines;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        return settings.Select(x => new MagazineSelectionInfo(x.Number, x.Name)).ToArray();
    }

    [HttpGet("{number:int}")]
    public async Task<MagazineInfo> RetrieveMagazineAsync(int number)
    {
        IReadOnlyList<MagazineSettings> settings = await this.settingsRetriever.RetrieveMagazineSettingsAsync();
        MagazineSettings magazineSettings = settings.Single(x => x.Number == number);
        return new MagazineInfo(
            magazineSettings.Number,
            magazineSettings.Name,
            magazineSettings.Places
                .Select(x => new PlaceInfo(x.Number))
                .ToArray());
    }
}