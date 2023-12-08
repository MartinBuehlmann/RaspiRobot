namespace RaspiRobot.RobotControl.Devices.Robot;

using RaspiRobot.RobotControl.Devices.Magazine;

public record PalletChuckLoading(StoragePlace Place, string? TagId) : IChuckLoading;