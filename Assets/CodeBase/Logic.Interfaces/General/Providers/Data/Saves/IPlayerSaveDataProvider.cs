using CodeBase.Data.Saves;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.Saves
{
    public interface IPlayerSaveDataProvider
    {
        ref CompanyLevelsSaveData GetCompanyLevelsData();
        void Save();
    }
}