using System;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class AudioSaveData
    {
        public float MusicVolume;
        public float SoundsVolume;

        public AudioSaveData()
        {
            MusicVolume = 1f;
            SoundsVolume = 1f;
        }
    }
}