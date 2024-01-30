namespace RaspiRobot.Web.Features.Devices.Storages.LoadingStations;

public record LoadingStationInfo(int Number, string Name, LoadingStationPlaceInfo[] Places);