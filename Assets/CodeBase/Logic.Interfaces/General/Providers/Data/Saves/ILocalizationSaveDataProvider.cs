namespace CodeBase.Logic.Interfaces.General.Providers.Data.Saves
{
    public interface ILocalizationSaveDataProvider
    {
        void SetLocale(string localeName);
        string GetLocale();
    }
}