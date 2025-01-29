using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.Localizations;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace CodeBase.Logic.General.Services.Localizations
{
    public class LocalizationService : ILocalizationService, IDisposable
    {
        private readonly ILocalizationSaveDataProvider _localizationSaveDataProvider;
        private readonly AsyncLazy _initializeTask;

        public event Action OnLocaleChanged;
        
        public LocalizationService(ILocalizationSaveDataProvider localizationSaveDataProvider)
        {
            _localizationSaveDataProvider = localizationSaveDataProvider;
            
            _initializeTask = UniTask.Lazy(InitializeAsync);
        }

        public void Dispose()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        public async UniTask SetLocaleAsync(string localeName)
        {
            await _initializeTask;
            
            var settings = LocalizationSettings.Instance;

            var locale = GetLocale(localeName);
            settings.SetSelectedLocale(locale);
            
            _localizationSaveDataProvider.SetLocale(localeName);
        }

        public async UniTask<string> GetLocaleAsync()
        {
            await _initializeTask;
            
            return LocalizationSettings.Instance.GetSelectedLocale().LocaleName;
        }

        public async UniTask<string> LocalizeAsync(string key, string tableId = LocalizationConstants.General)
        {
            await _initializeTask;
            
            var table = await GetTableAsync(tableId);
            var entry = table.GetEntry(key);

            if (entry != null)
            {
                return entry.GetLocalizedString();
            }

            UnityEngine.Debug.LogError("Not found localization key: " + key + " in table " + tableId);
            
            return string.Empty;
        }
        
        private static Locale GetLocale(string localeName)
        {
            var settings = LocalizationSettings.Instance;
            
            foreach (var locale in settings.GetAvailableLocales().Locales)
            {
                if (locale.LocaleName != localeName)
                    continue;
                
                return locale;
            }
            
            UnityEngine.Debug.LogError("Not found locale name " + localeName);
            
            return settings.GetSelectedLocale();
        }

        private async UniTask<StringTable> GetTableAsync(string tableId)
        {
            return await LocalizationSettings.StringDatabase.GetTableAsync(tableId).Task.AsUniTask();
        }

        private async UniTask InitializeAsync()
        {
            await LocalizationSettings.InitializationOperation.Task.AsUniTask();
            
            var localeName = _localizationSaveDataProvider.GetLocale();
            var locale = GetLocale(localeName);
            
            LocalizationSettings.Instance.SetSelectedLocale(locale);
            
            LocalizationSettings.Instance.OnSelectedLocaleChanged += OnSelectedLocaleChanged;
        }
        
        private void OnSelectedLocaleChanged(Locale locale)
        {
            OnLocaleChanged?.Invoke();
        }
    }
}