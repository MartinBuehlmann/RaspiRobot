namespace RaspiRobot.RobotControl.OperationMode;

public interface IOperationModeByHardwareRetriever
{
    OperationMode RetrieveOperationMode();
}