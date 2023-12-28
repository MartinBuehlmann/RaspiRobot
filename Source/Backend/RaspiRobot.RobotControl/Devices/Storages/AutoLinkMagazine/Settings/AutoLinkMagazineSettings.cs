namespace RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record AutoLinkMagazineSettings(int Number, string Name, PlaceSettings[] Places);