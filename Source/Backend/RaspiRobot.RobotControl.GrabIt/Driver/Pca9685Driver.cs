namespace RaspiRobot.RobotControl.GrabIt.Driver;

using System;
using System.Device.I2c;
using System.Threading;

/// <summary>
/// PCA9685 PWM LED/servo controller.
/// </summary>
internal class Pca9685Driver : IDisposable
{
    private const int BusId = 1;
    private const int DeviceAddress = 0x41;

    // Registers/etc:
    private const int Pca9685Address = 0x40;
    private const int Mode1 = 0x00;
    private const int Mode2 = 0x01;
    private const int SubAddress1 = 0x02;
    private const int SubAddress2 = 0x03;
    private const int SubAddress3 = 0x04;
    private const int PreScale = 0xFE;
    private const int Led0OnL = 0x06;
    private const int Led0OnH = 0x07;
    private const int Led0OffL = 0x08;
    private const int Led0OffH = 0x09;
    private const int AllLedOnL = 0xFA;
    private const int AllLedOnH = 0xFB;
    private const int AllLedOffL = 0xFC;
    private const int AllLedOffH = 0xFD;

    // Bits:
    private const int Restart = 0x80;
    private const int Sleep = 0x10;
    private const int Extosc = 0x40;
    private const int AllCall = 0x01;
    private const int Invert = 0x10;
    private const int OutDrv = 0x04;

    private I2cDevice? device;

    /// <summary>
    /// Setup I2C interface for the device.
    /// </summary>
    /// <exception cref="InvalidOperationException">thrown when driver already initialized.</exception>
    public void Initialize()
    {
        if (this.device is not null)
        {
            throw new InvalidOperationException("Device is already initialized.");
        }

        this.device = CreateI2CDevice(DeviceAddress);

        this.SetAllPwm(0, 0);
        var readBuffer = new byte[1];
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        this.device!.Write(new byte[] { Mode2, OutDrv });
        this.device!.Write(new byte[] { Mode1, AllCall });
        WaitForOscillator();
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        var mode1 = (byte)(readBuffer[0] & ~Sleep);
        this.device.Write(new byte[] { Mode1, mode1 });
        mode1 = (byte)(mode1 | Sleep);
        this.device!.Write(new byte[] { Mode1, mode1 });
        WaitForOscillator();
        mode1 = (byte)(mode1 | Extosc);
        this.device!.Write(new byte[] { Mode1, mode1 });
        WaitForOscillator();
        mode1 = (byte)(mode1 & ~Sleep);
        this.device!.Write(new byte[] { Mode1, mode1 });
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        WaitForOscillator();
    }

    /// <summary>
    /// Sends a software reset (SWRST) command to all servo drivers on the bus.
    /// </summary>
    public void SoftwareReset()
    {
        using I2cDevice i2CDevice = CreateI2CDevice(0x01);
        i2CDevice.Write(new byte[] { 0x00, 0x06 });
    }

    /// <summary>
    /// Set the PWM frequency to the provided value in hertz.
    /// </summary>
    /// <param name="frequency"></param>
    public void SetPwmFrequency(int frequency)
    {
        this.AssertInitialized();

        // 25 MHz
        double preScaleValue = 25000000.0;

        // 12-bit
        preScaleValue /= 4096.0;
        preScaleValue /= frequency;
        preScaleValue -= 1.0;
        var preScale = (byte)Math.Floor(preScaleValue + 0.5);
        var readBuffer = new byte[1];
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        byte oldMode = readBuffer[0];
        var newMode = (byte)((oldMode & 0x7F) | 0x10);
        this.device.Write(new byte[] { Mode1, newMode });
        this.device.Write(new byte[] { PreScale, preScale });
        this.device.WriteRead(new byte[] { PreScale }, new byte[1]);
        this.device.Write(new byte[] { Mode1, oldMode });
        WaitForOscillator();
        this.device.Write(new byte[] { Mode1, (byte)(oldMode | 0x80) });
    }

    /// <summary>
    /// Sets a single PWM channel.
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="on"></param>
    /// <param name="off"></param>
    public void SetPwm(byte channel, int on, int off)
    {
        this.AssertInitialized();

        this.device!.Write(new[] { (byte)(Led0OnL + (4 * channel)), (byte)(on & 0xFF) });
        this.device!.Write(new[] { (byte)(Led0OnH + (4 * channel)), (byte)(on >> 8) });
        this.device!.Write(new[] { (byte)(Led0OffL + (4 * channel)), (byte)(off & 0xFF) });
        this.device!.Write(new[] { (byte)(Led0OffH + (4 * channel)), (byte)(off >> 8) });
    }

    /// <summary>
    /// Sets all PWM channels.
    /// </summary>
    /// <param name="on"></param>
    /// <param name="off"></param>
    public void SetAllPwm(int on, int off)
    {
        this.AssertInitialized();

        this.device!.Write(new byte[] { AllLedOnL, (byte)(on & 0xFF) });
        this.device!.Write(new byte[] { AllLedOnH, (byte)(on >> 8) });
        this.device!.Write(new byte[] { AllLedOffL, (byte)(off & 0xFF) });
        this.device!.Write(new byte[] { AllLedOffH, (byte)(off >> 8) });
    }

    public void Dispose()
    {
        this.device?.Dispose();
        this.device = null;
    }

    private static I2cDevice CreateI2CDevice(int deviceAddress)
    {
        return I2cDevice.Create(new I2cConnectionSettings(BusId, deviceAddress));
    }

    private static void WaitForOscillator()
    {
        Thread.Sleep(5);
    }

    private void AssertInitialized()
    {
        if (this.device is null)
        {
            throw new InvalidOperationException("Device is not initialized.");
        }
    }
}