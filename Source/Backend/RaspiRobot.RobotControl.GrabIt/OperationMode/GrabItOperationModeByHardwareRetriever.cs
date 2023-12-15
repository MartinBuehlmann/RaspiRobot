namespace RaspiRobot.RobotControl.GrabIt.OperationMode;

using System;
using System.Device.Gpio;
using RaspiRobot.RobotControl.OperationMode;

internal class GrabItOperationModeByHardwareRetriever : IOperationModeByHardwareRetriever, IDisposable
{
    private const int OperationModeAutomaticPin = 22;
    private const int OperationModeMdiPin = 27;
    private readonly GpioController controller;

    public GrabItOperationModeByHardwareRetriever()
    {
        this.controller = new GpioController();
        this.controller.OpenPin(OperationModeAutomaticPin, PinMode.Input);
        this.controller.OpenPin(OperationModeMdiPin, PinMode.Input);
    }

    public OperationMode RetrieveOperationMode()
    {
        PinValue operationModeAutomaticPinValue = this.controller.Read(OperationModeAutomaticPin);
        PinValue operationModeMdiPinValue = this.controller.Read(OperationModeMdiPin);

        if (operationModeAutomaticPinValue == PinValue.Low && operationModeMdiPinValue == PinValue.Low)
        {
            return OperationMode.NotReady;
        }

        if (operationModeAutomaticPinValue == PinValue.High && operationModeMdiPinValue == PinValue.Low)
        {
            return OperationMode.Automatic;
        }

        if (operationModeAutomaticPinValue == PinValue.Low && operationModeMdiPinValue == PinValue.High)
        {
            return OperationMode.Mdi;
        }

        throw new InvalidOperationException("Having GPIO pin 22 and 27 high means an illegal OperationMode state.");
    }

    public void Dispose()
    {
        this.controller.ClosePin(OperationModeAutomaticPin);
        this.controller.ClosePin(OperationModeMdiPin);
    }
}