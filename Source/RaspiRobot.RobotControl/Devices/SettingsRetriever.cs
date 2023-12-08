namespace RaspiRobot.RobotControl.Devices;

using System.Linq;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Settings;

internal class SettingsRetriever : ISettingsRetriever
{
    private readonly CellSettingsLoader cellSettingsLoader;
    private CellSettings? cellSettings;

    public SettingsRetriever(CellSettingsLoader cellSettingsLoader)
    {
        this.cellSettingsLoader = cellSettingsLoader;
    }

    public async Task<RobotSettings> RetrieveRobotSettingsAsync()
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Robot;
    }

    public async Task<ChuckSettings> RetrieveByAsync(MachineChuck chuck)
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Machines
            .SelectMany(x => x.Chucks)
            .Single(c => c.Number == chuck.Number);
    }

    public async Task<PlaceSettings> RetrieveByAsync(StoragePlace place)
    {
        await this.EnsureCellSettingsLoadedAsync();
        return this.cellSettings!.Magazines
            .SelectMany(m => m.Places)
            .Single(p => p.Number == place.Number);
    }

    private async Task EnsureCellSettingsLoadedAsync()
    {
        if (this.cellSettings is null)
        {
            this.cellSettings = await this.cellSettingsLoader.RetrieveOrCreateAsync(nameof(CellSettings));
        }
    }
}