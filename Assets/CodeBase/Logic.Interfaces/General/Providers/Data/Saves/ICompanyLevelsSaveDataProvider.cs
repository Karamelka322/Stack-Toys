namespace CodeBase.Logic.Interfaces.General.Providers.Data.Saves
{
    public interface ICompanyLevelsSaveDataProvider
    {
        bool HasOpenedLevel(int index);
        bool HasClosedLevel(int index);
        bool HasCompletedLevel(int index);
        int GetCurrentLevel();
        int GetLastOpenedLevel();
        void SetCurrentLevel(int levelIndex);
        void SetLastOpenedLevel(int levelIndex);
        int GetNextLevelIndex();
        int GetNextLevelIndex(int currentLevelIndex);
    }
}