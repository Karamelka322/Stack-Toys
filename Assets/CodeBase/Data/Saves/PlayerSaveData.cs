using System;

namespace CodeBase.Data.Saves
{
    [Serializable]
    public class PlayerSaveData
    {
        public CompanyLevelsSaveData CompanyLevels = new();
    }
}