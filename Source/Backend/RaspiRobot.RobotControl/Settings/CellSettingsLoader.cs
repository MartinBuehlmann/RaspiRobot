namespace RaspiRobot.RobotControl.Settings;

using System.Linq;
using System.Threading.Tasks;
using DocumentStorage;

internal class CellSettingsLoader
{
    private readonly IDocumentStorage documentStorage;
    private readonly IDefaultCellSettingsProvider defaultCellSettingsProvider;

    public CellSettingsLoader(
        IDocumentStorage documentStorage,
        IJsonConverterProvider jsonConverterProvider,
        IDefaultCellSettingsProvider defaultCellSettingsProvider)
    {
        this.documentStorage = documentStorage;
        this.documentStorage.RegisterConverter(jsonConverterProvider.JsonConverters.ToArray());
        this.defaultCellSettingsProvider = defaultCellSettingsProvider;
    }

    public async Task<CellSettings> RetrieveOrCreateAsync(string cellSettingsFileName)
    {
        return await this.documentStorage.ReadAsync<CellSettings>(cellSettingsFileName) ??
               await this.CreateAsync(cellSettingsFileName);
    }

    private async Task<CellSettings> CreateAsync(string cellSettingsFileName)
    {
        CellSettings defaultCellSettings = this.defaultCellSettingsProvider.DefaultCellSettings;
        await this.documentStorage.WriteAsync(defaultCellSettings, cellSettingsFileName);
        return defaultCellSettings;
    }
}