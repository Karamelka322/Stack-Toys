using System.Linq;
using CodeBase.Data.Constants;
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
            
            TryInitData();
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
        
        public void SetTargetLevel(int levelIndex)
        {
            ref var data = ref _playerSaveDataProvider.GetCompanyLevelsData();
            data.TargetLevel = levelIndex;
        }
        
        public int GetTargetLevel()
        {
            return _playerSaveDataProvider.GetCompanyLevelsData().TargetLevel;
        }

        public int GetNextLevelIndex()
        {
            return Mathf.Clamp(_playerSaveDataProvider.GetCompanyLevelsData().CurrentLevel + 1, 0, CompanyConstants.NumberOfLevels);
        }
        
        public void SetCompletedLevel(int index)
        {
            ref var data = ref _playerSaveDataProvider.GetCompanyLevelsData();

            if (data.CompletedLevels.Contains(index) == false && index < CompanyConstants.NumberOfLevels - 1)
            {
                data.CompletedLevels.Add(index);
                data.TargetLevel = data.CompletedLevels.Max();
            }
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