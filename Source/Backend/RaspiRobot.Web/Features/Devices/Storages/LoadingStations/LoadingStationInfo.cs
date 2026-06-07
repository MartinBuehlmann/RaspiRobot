namespace RaspiRobot.Web.Features.Devices.Storages.LoadingStations;

public record LoadingStationInfo(string Identifier, string Name, LoadingStationPlaceInfo[] Places);