namespace RaspiRobot.RobotControl.Devices.Robot.Settings;

using RaspiRobot.RobotControl.Settings;

public record RobotSettings(string Name, Sequence HomingSequence, Sequence TransferSequence);