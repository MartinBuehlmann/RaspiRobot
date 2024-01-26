namespace RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record LoadingStationSettings(int Number, string Name, PlaceSettings[] Places);