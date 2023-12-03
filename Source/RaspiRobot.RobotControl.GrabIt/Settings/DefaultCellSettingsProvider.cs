namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(new RobotSettings("GrabIt", new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 99)}}}}))
        {
            Machines =
            {
                new MachineSettings("M1")
                {
                    Chucks =
                    {
                        new ChuckSettings(
                            1,
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 100)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 101)}}}}),
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
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 102)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 103)}}}}),
                    },
                },
                new MagazineSettings(2, "Mag")
                {
                    Places =
                    {
                        new PlaceSettings(
                            1,
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 104)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 105)}}}}),
                        new PlaceSettings(
                            2,
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 106)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 107)}}}}),
                        new PlaceSettings(
                            3,
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 106)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 107)}}}}),
                        new PlaceSettings(
                            4,
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 108)}}}},
                            new Sequence {Steps = {new Step {Positions = {new GrabItPosition(0, 109)}}}}),
                    },
                },
            },
        };
}