using System;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class PlayerSaveData
    {
        public SettingsSaveData Settings = new();
        public CompanyLevelsSaveData CompanyLevels = new();
    }
}