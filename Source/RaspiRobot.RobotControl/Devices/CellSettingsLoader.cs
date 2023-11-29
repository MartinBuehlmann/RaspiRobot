namespace RaspiRobot.RobotControl.Devices;

using System.Threading.Tasks;
using DocumentStorage;
using RaspiRobot.RobotControl.Settings;

public class CellSettingsLoader
{
    private readonly IDocumentStorage documentStorage;
    private readonly IDefaultCellSettingsProvider defaultCellSettingsProvider;

    public CellSettingsLoader(
        IDocumentStorage documentStorage,
        IDefaultCellSettingsProvider defaultCellSettingsProvider)
    {
        this.documentStorage = documentStorage;
        this.defaultCellSettingsProvider = defaultCellSettingsProvider;
    }

    public async Task<CellSettings> RetrieveOrCreateAsync(string cellSettingsFileName)
    {
        return await this.documentStorage.ReadAsync<CellSettings>(cellSettingsFileName) ??
               this.defaultCellSettingsProvider.DefaultCellSettings;
    }
}