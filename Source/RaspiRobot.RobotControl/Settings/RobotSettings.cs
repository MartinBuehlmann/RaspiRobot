namespace RaspiRobot.RobotControl.Settings;

public class RobotSettings
{
    public RobotSettings(string name, Position position)
    {
        this.Name = name;
        this.Position = position;
    }

    public string Name { get; }

    public Position Position { get; }
}