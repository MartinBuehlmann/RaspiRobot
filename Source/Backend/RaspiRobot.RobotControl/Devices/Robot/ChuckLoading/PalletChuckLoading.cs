namespace RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;

using RaspiRobot.RobotControl.Devices.Storages;

public record PalletChuckLoading(StoragePlace Place, string? TagId) : IChuckLoading;