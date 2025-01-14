using System;
using CodeBase.Logic.General.Services.Localizations;

namespace CodeBase.Data.General.Saves
{
    [Serializable]
    public class LocalizationSaveData
    {
        public string CurrentLocaleName;

        public LocalizationSaveData()
        {
            CurrentLocaleName = LocalizationConstants.EnglishLocal;
        }
    }
}