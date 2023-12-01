namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System;
using System.Device.I2c;
using System.Threading;

public class Pca9685Driver : IDisposable
{
    private const int BusId = 1;
    private const int DeviceAddress = 0x41;

    // Registers/etc:
    private const int PCA9685_ADDRESS = 0x40;
    private const int MODE1 = 0x00;
    private const int MODE2 = 0x01;
    private const int SUBADR1 = 0x02;
    private const int SUBADR2 = 0x03;
    private const int SUBADR3 = 0x04;
    private const int PRESCALE = 0xFE;
    private const int LED0_ON_L = 0x06;
    private const int LED0_ON_H = 0x07;
    private const int LED0_OFF_L = 0x08;
    private const int LED0_OFF_H = 0x09;
    private const int ALL_LED_ON_L = 0xFA;
    private const int ALL_LED_ON_H = 0xFB;
    private const int ALL_LED_OFF_L = 0xFC;
    private const int ALL_LED_OFF_H = 0xFD;

    // Bits:
    private const int RESTART = 0x80;
    private const int SLEEP = 0x10;
    private const int EXTOSC = 0x40;
    private const int ALLCALL = 0x01;
    private const int INVRT = 0x10;
    private const int OUTDRV = 0x04;

    private I2cDevice? device;

    public void Initialize()
    {
        if (this.device is not null)
        {
            throw new InvalidOperationException("Device is already initialized.");
        }

        this.device = CreateI2cDevice(DeviceAddress);

        this.SetAllPwm(0, 0);
        var readBuffer = new byte[1];
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        byte mode1 = readBuffer[0];
        this.device!.Write(new byte[] { MODE2, OUTDRV });
        this.device!.Write(new byte[] { MODE1, ALLCALL });
        Thread.Sleep(5);
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        mode1 = (byte)(readBuffer[0] & ~SLEEP);
        this.device.Write(new byte[] { MODE1, mode1 });
        mode1 = (byte)(mode1 | SLEEP);
        this.device!.Write(new byte[] { MODE1, mode1 });
        Thread.Sleep(5);
        mode1 = (byte)(mode1 | EXTOSC);
        this.device!.Write(new byte[] { MODE1, mode1 });
        Thread.Sleep(5);
        mode1 = (byte)(mode1 & ~SLEEP);
        this.device!.Write(new byte[] { MODE1, mode1 });
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        Thread.Sleep(5);
    }

    public void SoftwareReset()
    {
        using I2cDevice device = CreateI2cDevice(0x01);
        device.Write(new byte[] { 0x00, 0x06 });
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
        this.device!.WriteRead(new byte[] { MODE1 }, readBuffer);
        byte oldMode = readBuffer[0];
        byte newMode = (byte)((oldMode & 0x7F) | 0x10);
        this.device.Write(new byte[] { MODE1, newMode });
        this.device.Write(new byte[] { PRESCALE, preScale });
        this.device.WriteRead(new byte[] { PRESCALE }, new byte[1]);
        this.device.Write(new byte[] { MODE1, oldMode });
        Thread.Sleep(5);
        this.device.Write(new byte[] { MODE1, (byte)(oldMode | 0x80) });
    }

    public void SetPwm(byte channel, int on, int off)
    {
        this.AssertInitialized();

        this.device!.Write(new byte[] { (byte)(LED0_ON_L + 4 * channel), (byte)(on & 0xFF) });
        this.device!.Write(new byte[] { (byte)(LED0_ON_H + 4 * channel), (byte)(on >> 8) });
        this.device!.Write(new byte[] { (byte)(LED0_OFF_L + 4 * channel), (byte)(off & 0xFF) });
        this.device!.Write(new byte[] { (byte)(LED0_OFF_H + 4 * channel), (byte)(off >> 8) });
    }

    public void SetAllPwm(int on, int off)
    {
        this.AssertInitialized();

        this.device!.Write(new byte[] { ALL_LED_ON_L, (byte)(on & 0xFF) });
        this.device!.Write(new byte[] { ALL_LED_ON_H, (byte)(on >> 8) });
        this.device!.Write(new byte[] { ALL_LED_OFF_L, (byte)(off & 0xFF) });
        this.device!.Write(new byte[] { ALL_LED_OFF_H, (byte)(off >> 8) });
    }

    public void Dispose()
    {
        this.device?.Dispose();
        this.device = null;
    }

    private static I2cDevice CreateI2cDevice(int deviceAddress)
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