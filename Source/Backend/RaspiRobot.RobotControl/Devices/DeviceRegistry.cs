namespace RaspiRobot.RobotControl.Devices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class DeviceRegistry
{
    private readonly DeviceCreator deviceCreator;
    private readonly List<IDevice> devices = new();

    public DeviceRegistry(DeviceCreator deviceCreator)
    {
        this.deviceCreator = deviceCreator;
    }

    public async Task InitializeAsync()
    {
        this.devices.AddRange(await this.deviceCreator.CreateAllAsync());
    }

    public TDevice Retrieve<TDevice>(Func<TDevice, bool> filter)
        where TDevice : IDevice
    {
        return this.devices.OfType<TDevice>().Single(filter);
    }

    public IReadOnlyList<TDevice> RetrieveAll<TDevice>()
    {
        return this.devices.OfType<TDevice>().ToList();
    }
}