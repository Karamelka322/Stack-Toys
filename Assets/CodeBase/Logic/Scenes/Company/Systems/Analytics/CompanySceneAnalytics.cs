using System;
using CodeBase.Logic.General.Services.Analytics;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Finish;

namespace CodeBase.Logic.Scenes.Company.Systems.Analytics
{
    public class CompanySceneAnalytics : IDisposable
    {
        private readonly IAnalyticService _analyticService;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ICompanyFinishSystem _companyFinishSystem;
        private readonly IToyDestroyer _toyDestroyer;

        public CompanySceneAnalytics(
            IAnalyticService analyticService,
            ICompanyFinishSystem companyFinishSystem,
            IToyDestroyer toyDestroyer,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyFinishSystem = companyFinishSystem;
            _toyDestroyer = toyDestroyer;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _analyticService = analyticService;

            _companyFinishSystem.OnLevelComplete += OnLevelComplete;
            _toyDestroyer.OnDestroyAll += OnDestroyToys;
        }

        public void Dispose()
        {
            _companyFinishSystem.OnLevelComplete -= OnLevelComplete;
            _toyDestroyer.OnDestroyAll -= OnDestroyToys;
        }

        private void OnLevelComplete(int levelIndex)
        {
            _analyticService.RegisterEvent($"{AnalyticConstants.CompanyLevelFinishedEvent}_{levelIndex + 1}");
        }

        private void OnDestroyToys()
        {
            var levelNumber = _companyLevelsSaveDataProvider.GetCurrentLevel() + 1;
            
            _analyticService.RegisterEvent($"{AnalyticConstants.CompanyLevelFinishedEvent}_{levelNumber}");
        }
    }
}