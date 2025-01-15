using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class CompanyLevelsSaveDataProvider : ICompanyLevelsSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;
        
        public CompanyLevelsSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
        }
        
        public int GetCurrentLevel()
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().CurrentLevel;
        }
        
        public void SetCurrentLevel(int levelIndex)
        {
            ref var data = ref _playerSaveDataProvider.GetCompanyLevelsData();
            data.CurrentLevel = levelIndex;
        }
        
        public void SetLastOpenedLevel(int levelIndex)
        {
            ref var data = ref _playerSaveDataProvider.GetCompanyLevelsData();
            data.LastOpenedLevel = levelIndex;
        }
        
        public int GetLastOpenedLevel()
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().LastOpenedLevel;
        }

        public int GetNextLevelIndex()
        {
            var currentLevel = _playerSaveDataProvider.GetCompanyLevelsData().CurrentLevel;
            return GetNextLevelIndex(currentLevel);
        }
        
        public int GetNextLevelIndex(int currentLevelIndex)
        {
            return Mathf.Clamp(currentLevelIndex + 1, 0, CompanyConstants.NumberOfLevels - 1);
        }

        public bool HasOpenedLevel(int index)
        {
            return index <= _playerSaveDataProvider.GetCompanyLevelsData().LastOpenedLevel;
        }

        public bool HasClosedLevel(int index)
        {
            return index > _playerSaveDataProvider.GetCompanyLevelsData().LastOpenedLevel;
        }

        public bool HasCompletedLevel(int index)
        {
            return index < _playerSaveDataProvider.GetCompanyLevelsData().LastOpenedLevel;
        }
    }
}