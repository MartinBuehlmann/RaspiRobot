namespace RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

using RaspiRobot.RobotControl.Devices.Storages;

public record PalletChuckOccupancy(StoragePlace Place, string? TagId) : IChuckOccupancy;