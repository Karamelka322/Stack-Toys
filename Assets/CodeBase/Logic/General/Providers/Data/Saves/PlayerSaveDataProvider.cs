using CodeBase.Data.General.Saves;
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
            _data.CompanyLevels ??= new CompanyLevelsSaveData();
            return ref _data.CompanyLevels;
        }
        
        public ref AudioSaveData GetAudioData()
        {
            _data.Audio ??= new AudioSaveData();
            return ref _data.Audio;
        }
        
        public ref LocalizationSaveData GetLocalizationData()
        {
            _data.Localization ??= new LocalizationSaveData();
            return ref _data.Localization;
        }
        
        public void Save()
        {
            _saveLoadService.Save(_data);
        }
    }
}