namespace RaspiRobot.RobotControl.Devices.Robot.Settings;

using RaspiRobot.RobotControl.Settings;

public record RobotSettings(string Name, SequenceSettings HomingSequence, SequenceSettings TransferSequence);