using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class CompanyLevelsSaveDataProvider : ICompanyLevelsSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;
        
        public CompanyLevelsSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
            
            TryInitData();
        }

        public bool HasOpenedLevel(int index)
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().OpenedLevels.Contains(index);
        }

        public bool HasClosedLevel(int index)
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().ClosedLevels.Contains(index);
        }

        public bool HasCompletedLevel(int index)
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().CompletedLevels.Contains(index);
        }

        private void TryInitData()
        {
            ref var levelsData = ref _playerSaveDataProvider.GetCompanyLevelsData();

            if (levelsData.OpenedLevels.Count == 0)
            {
                levelsData.OpenedLevels.Add(0);
            }
            
            for (int i = 1; i < CompanyConstants.NumberOfLevels; i++)
            {
                levelsData.ClosedLevels.Add(i);
            }
        }
    }
}