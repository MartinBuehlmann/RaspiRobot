namespace RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record AutoLinkMagazineSettings(string Identifier, string Name, PlaceSettings[] Places);