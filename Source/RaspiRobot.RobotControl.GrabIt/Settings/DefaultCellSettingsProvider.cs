namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(new RobotSettings("GrabIt",
            new Sequence
            {
                Steps =
                {
                    new Step
                    {
                        Positions =
                        {
                            new GrabItPosition(0, 112),
                            new GrabItPosition(1, 112),
                            new GrabItPosition(2, 112),
                            new GrabItPosition(3, 112),
                            new GrabItPosition(4, 112),
                            new GrabItPosition(5, 112),
                        },
                    },
                },
            }))
        {
            Machines =
            {
                new MachineSettings("M1")
                {
                    Chucks =
                    {
                        new ChuckSettings(
                            1,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 100),
                                            new GrabItPosition(1, 100),
                                            new GrabItPosition(2, 100),
                                            new GrabItPosition(3, 100),
                                            new GrabItPosition(4, 100),
                                            new GrabItPosition(5, 100),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 101),
                                            new GrabItPosition(1, 101),
                                            new GrabItPosition(2, 101),
                                            new GrabItPosition(3, 101),
                                            new GrabItPosition(4, 101),
                                            new GrabItPosition(5, 101),
                                        },
                                    },
                                },
                            }),
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
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 102),
                                            new GrabItPosition(1, 102),
                                            new GrabItPosition(2, 102),
                                            new GrabItPosition(3, 102),
                                            new GrabItPosition(4, 102),
                                            new GrabItPosition(5, 102),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 103),
                                            new GrabItPosition(1, 103),
                                            new GrabItPosition(2, 103),
                                            new GrabItPosition(3, 103),
                                            new GrabItPosition(4, 103),
                                            new GrabItPosition(5, 103),
                                        },
                                    },
                                },
                            }),
                    },
                },
                new MagazineSettings(2, "Mag")
                {
                    Places =
                    {
                        new PlaceSettings(
                            1,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 104),
                                            new GrabItPosition(1, 104),
                                            new GrabItPosition(2, 104),
                                            new GrabItPosition(3, 104),
                                            new GrabItPosition(4, 104),
                                            new GrabItPosition(5, 104),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 105),
                                            new GrabItPosition(1, 105),
                                            new GrabItPosition(2, 105),
                                            new GrabItPosition(3, 105),
                                            new GrabItPosition(4, 105),
                                            new GrabItPosition(5, 105),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            2,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 106),
                                            new GrabItPosition(1, 106),
                                            new GrabItPosition(2, 106),
                                            new GrabItPosition(3, 106),
                                            new GrabItPosition(4, 106),
                                            new GrabItPosition(5, 106),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 107),
                                            new GrabItPosition(1, 107),
                                            new GrabItPosition(2, 107),
                                            new GrabItPosition(3, 107),
                                            new GrabItPosition(4, 107),
                                            new GrabItPosition(5, 107),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            3,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 108),
                                            new GrabItPosition(1, 108),
                                            new GrabItPosition(2, 108),
                                            new GrabItPosition(3, 108),
                                            new GrabItPosition(4, 108),
                                            new GrabItPosition(5, 108),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 109),
                                            new GrabItPosition(1, 109),
                                            new GrabItPosition(2, 109),
                                            new GrabItPosition(3, 109),
                                            new GrabItPosition(4, 109),
                                            new GrabItPosition(5, 109),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            4,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 110),
                                            new GrabItPosition(1, 110),
                                            new GrabItPosition(2, 110),
                                            new GrabItPosition(3, 110),
                                            new GrabItPosition(4, 110),
                                            new GrabItPosition(5, 110),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 111),
                                            new GrabItPosition(1, 111),
                                            new GrabItPosition(2, 111),
                                            new GrabItPosition(3, 111),
                                            new GrabItPosition(4, 111),
                                            new GrabItPosition(5, 111),
                                        },
                                    },
                                },
                            }),
                    },
                },
            },
        };
}