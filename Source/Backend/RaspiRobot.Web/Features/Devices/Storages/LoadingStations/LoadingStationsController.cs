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
        return settings.Select(x => new LoadingStationSelectionInfo(x.Identifier, x.Name)).ToArray();
    }

    [HttpGet("{identifier}")]
    public async Task<LoadingStationInfo> RetrieveMagazineAsync(string identifier)
    {
        IReadOnlyList<LoadingStationSettings> settings = await this.settingsRetriever.RetrieveLoadingStationSettingsAsync();
        LoadingStationSettings? loadingStationSettings = settings.SingleOrDefault(x => x.Identifier == identifier);

        if (loadingStationSettings is not null)
        {
            return new LoadingStationInfo(
                loadingStationSettings.Identifier,
                loadingStationSettings.Name,
                loadingStationSettings.Places
                    .Select(x => new LoadingStationPlaceInfo(x.Identifier))
                    .ToArray());
        }

        throw new ResourceNotFoundException($"No loading station found with number {identifier}.");
    }
}