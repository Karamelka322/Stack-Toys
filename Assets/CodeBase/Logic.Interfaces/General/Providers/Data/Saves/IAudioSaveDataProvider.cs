namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public interface IAudioSaveDataProvider
    {
        void SetMusicVolume(float volume);
        void SetSoundsVolume(float volume);
        float GetMusicVolume();
        float GetSoundsVolume();
        bool IsMusicVolumeMute();
        bool IsSoundsVolumeMute();
    }
}