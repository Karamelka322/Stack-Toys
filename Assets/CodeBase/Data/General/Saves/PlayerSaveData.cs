using System;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class PlayerSaveData
    {
        public CompanyLevelsSaveData CompanyLevels = new();
    }
}