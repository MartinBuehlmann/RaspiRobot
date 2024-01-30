namespace RaspiRobot.Web.Features.Devices.Storages.LoadingStations;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Settings;

public class LoadingStationsController : WebController
{
    private readonly ISettingsRetriever settingsRetriever;

    public LoadingStationsController(ISettingsRetriever settingsRetriever)
    {
        this.settingsRetriever = settingsRetriever;
    }

    [HttpGet]
    public async Task<LoadingStationSelectionInfo[]> RetrieveMagazineSelectionsAsync()
    {
        IReadOnlyList<LoadingStationSettings> settings = await this.settingsRetriever.RetrieveLoadingStationSettingsAsync();
        return settings.Select(x => new LoadingStationSelectionInfo(x.Number, x.Name)).ToArray();
    }

    [HttpGet("{number:int}")]
    public async Task<LoadingStationInfo> RetrieveMagazineAsync(int number)
    {
        IReadOnlyList<LoadingStationSettings> settings = await this.settingsRetriever.RetrieveLoadingStationSettingsAsync();
        LoadingStationSettings? loadingStationSettings = settings.SingleOrDefault(x => x.Number == number);

        if (loadingStationSettings is not null)
        {
            return new LoadingStationInfo(
                loadingStationSettings.Number,
                loadingStationSettings.Name,
                loadingStationSettings.Places
                    .Select(x => new LoadingStationPlaceInfo(x.Number))
                    .ToArray());
        }

        throw new ResourceNotFoundException($"No loading station found with number {number}.");
    }
}