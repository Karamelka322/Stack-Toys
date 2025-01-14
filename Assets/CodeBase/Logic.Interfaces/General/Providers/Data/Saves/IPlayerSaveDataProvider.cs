using CodeBase.Data.General.Saves;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.Saves
{
    public interface IPlayerSaveDataProvider
    {
        ref CompanyLevelsSaveData GetCompanyLevelsData();
        ref SettingsSaveData GetSettingsData();
        
        void Save();
    }
}