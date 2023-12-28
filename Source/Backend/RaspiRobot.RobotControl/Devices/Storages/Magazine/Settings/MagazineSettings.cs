namespace RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;

using RaspiRobot.RobotControl.Devices.Storages.Settings;

public record MagazineSettings(int Number, string Name, PlaceSettings[] Places);