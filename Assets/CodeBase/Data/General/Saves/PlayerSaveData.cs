using System;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class PlayerSaveData
    {
        public AudioSaveData Audio = new();
        public LocalizationSaveData Localization = new();
        public CompanyLevelsSaveData CompanyLevels = new();
    }
}