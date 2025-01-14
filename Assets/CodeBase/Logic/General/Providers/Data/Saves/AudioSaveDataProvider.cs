using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class AudioSaveDataProvider : IAudioSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public AudioSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
        }

        public void SetMusicVolume(float volume)
        {
            ref var settingsData = ref _playerSaveDataProvider.GetAudioData();
            settingsData.MusicVolume = volume;
        }
        
        public void SetSoundsVolume(float volume)
        {
            ref var settingsData = ref _playerSaveDataProvider.GetAudioData();
            settingsData.SoundsVolume = volume;
        }

        public float GetMusicVolume()
        {
            return _playerSaveDataProvider.GetAudioData().MusicVolume;
        }
        
        public float GetSoundsVolume()
        {
            return _playerSaveDataProvider.GetAudioData().SoundsVolume;
        }
        
        public bool IsMusicVolumeMute()
        {
            return _playerSaveDataProvider.GetAudioData().MusicVolume < 0.01f;
        }
        
        public bool IsSoundsVolumeMute()
        {
            return _playerSaveDataProvider.GetAudioData().SoundsVolume < 0.01f;
        }
    }
}