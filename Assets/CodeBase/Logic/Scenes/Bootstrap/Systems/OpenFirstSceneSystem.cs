using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using UniRx;

namespace CodeBase.Logic.Scenes.Bootstrap.Systems
{
    public class OpenFirstSceneSystem : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

        public OpenFirstSceneSystem(
            ISceneReadyObserver sceneReadyObserver,
            ISceneLoadService sceneLoadService,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _sceneLoadService = sceneLoadService;
            
            _disposable = sceneReadyObserver.IsReady.Subscribe(OnSceneReady);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnSceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }

            if (_companyLevelsSaveDataProvider.HasClosedLevel(1))
            {
                _sceneLoadService.LoadScene(SceneNames.Company);
            }
            else
            {
                _sceneLoadService.LoadScene(SceneNames.Menu);
            }
        }
    }
}