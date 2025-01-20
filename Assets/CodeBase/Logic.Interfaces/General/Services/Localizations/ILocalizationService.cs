using System;
using CodeBase.Data.General.Constants;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Services.Localizations
{
    public interface ILocalizationService
    {
        event Action OnLocaleChanged;

        UniTask SetLocaleAsync(string localeName);
        UniTask<string> GetLocaleAsync();
        
        UniTask<string> LocalizeAsync(string key, string tableId = LocalizationConstants.General);
    }
}