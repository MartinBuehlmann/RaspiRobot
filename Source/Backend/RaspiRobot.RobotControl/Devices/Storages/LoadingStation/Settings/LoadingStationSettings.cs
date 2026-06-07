namespace RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record LoadingStationSettings(string Identifier, string Name, PlaceSettings[] Places);