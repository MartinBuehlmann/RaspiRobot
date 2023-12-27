namespace RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;

using RaspiRobot.RobotControl.Devices.Machines;

public record ChuckLoading(MachineChuck Chuck, IChuckLoading Loading);