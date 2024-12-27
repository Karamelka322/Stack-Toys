using CodeBase.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SaveLoad;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class PlayerSaveDataProvider : IPlayerSaveDataProvider
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly PlayerSaveData _data;
        
        public PlayerSaveDataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _data = _saveLoadService.Load<PlayerSaveData>();
        }
        
        public ref CompanyLevelsSaveData GetCompanyLevelsData()
        {
            return ref _data.CompanyLevels;
        }
        
        public void Save()
        {
            _saveLoadService.Save(_data);
        }
    }
}