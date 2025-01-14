using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class SettingsSaveDataProvider : ISettingsSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public SettingsSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
        }

        public void SetMusicVolume(float volume)
        {
            ref var settingsData = ref _playerSaveDataProvider.GetSettingsData();
            settingsData.MusicVolume = volume;
        }
        
        public void SetSoundsVolume(float volume)
        {
            ref var settingsData = ref _playerSaveDataProvider.GetSettingsData();
            settingsData.SoundsVolume = volume;
        }

        public float GetMusicVolume()
        {
            return _playerSaveDataProvider.GetSettingsData().MusicVolume;
        }
        
        public float GetSoundsVolume()
        {
            return _playerSaveDataProvider.GetSettingsData().SoundsVolume;
        }
        
        public bool IsMusicVolumeMute()
        {
            return _playerSaveDataProvider.GetSettingsData().MusicVolume < 0.01f;
        }
        
        public bool IsSoundsVolumeMute()
        {
            return _playerSaveDataProvider.GetSettingsData().SoundsVolume < 0.01f;
        }
    }
}