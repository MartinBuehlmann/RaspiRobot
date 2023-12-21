namespace RaspiRobot.RobotControl.Devices.Robot.Settings;

using RaspiRobot.RobotControl.Settings;

public class RobotSettings
{
    public RobotSettings(string name, Sequence homingSequence, Sequence transferSequence)
    {
        this.Name = name;
        this.HomingSequence = homingSequence;
        this.TransferSequence = transferSequence;
    }

    public string Name { get; }

    /// <summary>
    /// Gets a sequence which gets executed by the robot after the system was started.
    /// t is recommended to move all drives so that they are at a defined position.
    /// </summary>
    public Sequence HomingSequence { get; }

    /// <summary>
    /// Gets the sequence that gets executed after pick- or place-sequence.
    /// It is possible to move just the specific drives to be out of the storage or machine
    /// and letting other drives like axis 0 on its original position.
    /// </summary>
    public Sequence TransferSequence { get; }
}