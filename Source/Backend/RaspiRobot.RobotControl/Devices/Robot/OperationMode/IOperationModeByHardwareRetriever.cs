namespace RaspiRobot.RobotControl.Devices.Robot.OperationMode;

public interface IOperationModeByHardwareRetriever
{
    OperationMode RetrieveOperationMode();
}