namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(
            new RobotSettings(
                "GrabIt",
                new Sequence
                {
                    Steps =
                    {
                        new Step
                        {
                            Positions =
                            {
                                new GrabItPosition(2, 300),
                                new GrabItPosition(4, 300),
                                new GrabItPosition(5, 320),
                                new GrabItPosition(0, 300),
                                new GrabItPosition(1, 300),
                                new GrabItPosition(3, 200),
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
                                new GrabItPosition(1, 300),
                                new GrabItPosition(2, 300),
                                new GrabItPosition(3, 200),
                                new GrabItPosition(4, 300),
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
                                            new GrabItPosition(0, 510),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 510),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 95),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 95),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                            2,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 160),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 160),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 250),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 250),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 350),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 350),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            5,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(0, 440),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
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
                                            new GrabItPosition(0, 440),
                                            new GrabItPosition(1, 300),
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(3, 200),
                                            new GrabItPosition(4, 300),
                                            new GrabItPosition(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(1, 250),
                                            new GrabItPosition(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new GrabItPosition(2, 300),
                                            new GrabItPosition(1, 300),
                                        },
                                    },
                                },
                            }),
                    },
                },
            },
        };
}