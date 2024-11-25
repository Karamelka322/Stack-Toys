using System;
using System.Collections.Generic;

namespace CodeBase.Data.Saves
{
    [Serializable]
    public class CompanyLevelsSaveData
    {
        public int CurrentLevel;
        
        public List<int> CompletedLevels = new();
        public List<int> ClosedLevels = new();
        public List<int> OpenedLevels = new();
    }
}