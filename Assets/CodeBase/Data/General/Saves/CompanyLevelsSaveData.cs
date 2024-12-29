using System;
using System.Collections.Generic;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class CompanyLevelsSaveData
    {
        public int CurrentLevel;
        public int TargetLevel;
        
        public List<int> CompletedLevels = new();
        public List<int> ClosedLevels = new();
        public List<int> OpenedLevels = new();
    }
}