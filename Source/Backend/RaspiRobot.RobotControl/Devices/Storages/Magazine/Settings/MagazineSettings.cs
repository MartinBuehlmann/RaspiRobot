namespace RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record MagazineSettings(string Identifier, string Name, PlaceSettings[] Places);