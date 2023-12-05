namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

internal class SequencesExecutor
{
    public void Execute(IReadOnlyList<Sequence> sequences, IGrabItDriver driver)
    {
        IReadOnlyDictionary<byte, int> currentDrivePositions = driver.CurrentDrivePositions;

        foreach (Sequence sequence in sequences)
        {
            foreach (Step step in sequence.Steps)
            {
                // TODO: Interpolate movements
            }
        }
        
        driver.Execute(sequences);
    }
}