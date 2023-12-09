namespace RaspiRobot.RobotControl.Devices.Robot;

using RaspiRobot.RobotControl.Devices.Storages;

public record PalletChuckLoading(StoragePlace Place, string? TagId) : IChuckLoading;