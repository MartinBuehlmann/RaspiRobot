namespace RaspiRobot.RobotControl.Devices.Robot;

using RaspiRobot.RobotControl.Devices.Machines;

public record ChuckLoading(MachineChuck Chuck, IChuckLoading Loading);