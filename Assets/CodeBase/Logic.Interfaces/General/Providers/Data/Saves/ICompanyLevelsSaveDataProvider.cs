namespace CodeBase.Logic.Interfaces.General.Providers.Data.Saves
{
    public interface ICompanyLevelsSaveDataProvider
    {
        bool HasOpenedLevel(int index);
        bool HasClosedLevel(int index);
        bool HasCompletedLevel(int index);
        int GetCurrentLevel();
        void SetCompletedLevel(int index);
        int GetTargetLevel();
        void SetCurrentLevel(int levelIndex);
        void SetTargetLevel(int levelIndex);
        int GetNextLevelIndex();
    }
}