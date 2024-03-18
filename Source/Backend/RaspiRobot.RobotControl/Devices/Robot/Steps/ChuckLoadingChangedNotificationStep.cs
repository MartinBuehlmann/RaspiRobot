namespace RaspiRobot.RobotControl.Devices.Robot.Steps;

using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;

public record ChuckLoadingChangedNotificationStep(ChuckLoading ChuckLoading) : IStep
{
    public static ChuckLoadingChangedNotificationStep Occupied(MachineChuck chuck, PalletChuckLoading palletChuckLoading)
        => new ChuckLoadingChangedNotificationStep(new ChuckLoading(chuck, palletChuckLoading));

    public static ChuckLoadingChangedNotificationStep Empty(MachineChuck chuck)
        => new ChuckLoadingChangedNotificationStep(new ChuckLoading(chuck, new EmptyChuckLoading()));
}