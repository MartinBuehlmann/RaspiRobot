namespace RaspiRobot.RobotControl.Settings;

public class PlaceSettings
{
    public PlaceSettings(int number, Position position)
    {
        this.Number = number;
        this.Position = position;
    }

    public int Number { get; }

    public Position Position { get; }
}