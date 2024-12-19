namespace RaspiRobot.RobotControl.Devices.Robot.Steps;

using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

public record ChuckLoadingChangedNotificationStep(ChuckOccupancy ChuckOccupancy) : IStep
{
    public static ChuckLoadingChangedNotificationStep Occupied(MachineChuck chuck, PalletChuckOccupancy palletChuckOccupancy)
        => new ChuckLoadingChangedNotificationStep(new ChuckOccupancy(chuck, palletChuckOccupancy));

    public static ChuckLoadingChangedNotificationStep Empty(MachineChuck chuck)
        => new ChuckLoadingChangedNotificationStep(new ChuckOccupancy(chuck, new EmptyChuckOccupancy()));
}