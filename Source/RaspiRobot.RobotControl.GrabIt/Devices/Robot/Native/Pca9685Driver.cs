namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot.Native;

using System;
using System.Device.I2c;
using System.Threading;

public class Pca9685Driver : IDisposable
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
        Thread.Sleep(5);
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        byte mode1 = (byte)(readBuffer[0] & ~Sleep);
        this.device.Write(new byte[] { Mode1, mode1 });
        mode1 = (byte)(mode1 | Sleep);
        this.device!.Write(new byte[] { Mode1, mode1 });
        Thread.Sleep(5);
        mode1 = (byte)(mode1 | Extosc);
        this.device!.Write(new byte[] { Mode1, mode1 });
        Thread.Sleep(5);
        mode1 = (byte)(mode1 & ~Sleep);
        this.device!.Write(new byte[] { Mode1, mode1 });
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        Thread.Sleep(5);
    }

    public void SoftwareReset()
    {
        using I2cDevice i2CDevice = CreateI2CDevice(0x01);
        i2CDevice.Write(new byte[] { 0x00, 0x06 });
    }

    public void SetPwmFrequency(int frequency)
    {
        this.AssertInitialized();

        double preScaleValue = 25000000.0;
        preScaleValue /= 4096.0;
        preScaleValue /= (double)frequency;
        preScaleValue -= 1.0;
        byte preScale = (byte)Math.Floor(preScaleValue + 0.5);
        var readBuffer = new byte[1];
        this.device!.WriteRead(new byte[] { Mode1 }, readBuffer);
        byte oldMode = readBuffer[0];
        byte newMode = (byte)((oldMode & 0x7F) | 0x10);
        this.device.Write(new byte[] { Mode1, newMode });
        this.device.Write(new byte[] { PreScale, preScale });
        this.device.WriteRead(new byte[] { PreScale }, new byte[1]);
        this.device.Write(new byte[] { Mode1, oldMode });
        Thread.Sleep(5);
        this.device.Write(new byte[] { Mode1, (byte)(oldMode | 0x80) });
    }

    public void SetPwm(byte channel, int on, int off)
    {
        this.AssertInitialized();

        this.device!.Write(new byte[] { (byte)(Led0OnL + (4 * channel)), (byte)(on & 0xFF) });
        this.device!.Write(new byte[] { (byte)(Led0OnH + (4 * channel)), (byte)(on >> 8) });
        this.device!.Write(new byte[] { (byte)(Led0OffL + (4 * channel)), (byte)(off & 0xFF) });
        this.device!.Write(new byte[] { (byte)(Led0OffH + (4 * channel)), (byte)(off >> 8) });
    }

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

    private void AssertInitialized()
    {
        if (this.device is null)
        {
            throw new InvalidOperationException("Device is not initialized.");
        }
    }
}