using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class LocalizationSaveDataProvider : ILocalizationSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public LocalizationSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
        }

        public void SetLocale(string localeName)
        {
            ref var data = ref _playerSaveDataProvider.GetLocalizationData();
            data.CurrentLocaleName = localeName;
        }

        public string GetLocale()
        {
            return _playerSaveDataProvider.GetLocalizationData().CurrentLocaleName;
        }
    }
}