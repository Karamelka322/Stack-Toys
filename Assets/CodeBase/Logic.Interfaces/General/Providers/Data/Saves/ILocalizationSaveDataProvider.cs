namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public interface ILocalizationSaveDataProvider
    {
        void SetLocale(string localeName);
        string GetLocale();
    }
}