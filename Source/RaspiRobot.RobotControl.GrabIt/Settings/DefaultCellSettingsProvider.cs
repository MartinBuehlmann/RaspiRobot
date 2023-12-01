namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(new RobotSettings("GrabIt", new Sequence()))
        {
            Machines =
            {
                new MachineSettings("M1")
                {
                    Chucks =
                    {
                        new ChuckSettings(
                            0,
                            new Sequence { Steps = { new Step { Positions = { new GrabItPosition(0, 100) } } } },
                            new Sequence { Steps = { new Step { Positions = { new GrabItPosition(0, 100) } } } }),
                    },
                },
            },
            Magazines =
            {
                new MagazineSettings(1, "LS")
                {
                    Places =
                    {
                        new PlaceSettings(
                            1,
                            new Sequence(),
                            new Sequence()),
                    },
                },
                new MagazineSettings(2, "Mag")
                {
                    Places =
                    {
                        new PlaceSettings(1, new Sequence(), new Sequence()),
                        new PlaceSettings(2, new Sequence(), new Sequence()),
                        new PlaceSettings(3, new Sequence(), new Sequence()),
                        new PlaceSettings(4, new Sequence(), new Sequence()),
                    },
                },
            },
        };
}