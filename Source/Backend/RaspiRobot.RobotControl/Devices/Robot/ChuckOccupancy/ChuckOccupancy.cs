namespace RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;

using RaspiRobot.RobotControl.Devices.Machines;

public record ChuckOccupancy(MachineChuck Chuck, IChuckOccupancy Occupancy);