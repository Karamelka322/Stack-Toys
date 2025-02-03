using System;
using System.Threading.Tasks;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
{
    public interface ICompanyFinishSystem
    {
        event Action<int> OnLevelComplete;
    }

    public class CompanyFinishSystem : IDisposable, ICompanyFinishSystem
    {
        private const float _delayForOpenNextScene = 4f;
        
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IDisposable _disposable;
        private readonly ISceneLoadService _sceneLoadService;

        public event Action<int> OnLevelComplete;
        
        public CompanyFinishSystem(
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            IFinishObserver finishObserver,
            ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;

            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }

        private async void OnFinishValueChanged(bool isFinish)
        {
            if (isFinish == false)
            {
                return;
            }

            var currentOpenedLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var nextLevel = _companyLevelsSaveDataProvider.GetNextLevelIndex(currentOpenedLevel);
            
            if (nextLevel > _companyLevelsSaveDataProvider.GetLastOpenedLevel())
            {
                _companyLevelsSaveDataProvider.SetLastOpenedLevel(nextLevel);
            }
            
            _companyLevelsSaveDataProvider.SetCurrentLevel(nextLevel);
            
            OnLevelComplete?.Invoke(currentOpenedLevel);
            
            await OpenNextSceneAsync(currentOpenedLevel);
        }

        private async Task OpenNextSceneAsync(int currentOpenedLevelIndex)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayForOpenNextScene));
            
            if (currentOpenedLevelIndex + 1 == CompanyConstants.NumberOfLevels)
            {
                _sceneLoadService.LoadSceneAsync(SceneNames.Infinity, 1f).Forget();
            }
            else
            {
                _sceneLoadService.ReloadSceneAsync(1f).Forget();
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}