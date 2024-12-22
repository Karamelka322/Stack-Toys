using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyLoadingWindowPresenter : IDisposable
    {
        private readonly ILoadingWindow _loadingWindow;
        private readonly IDisposable _disposable;
        private readonly ISceneLoadService _sceneLoadService;

        public CompanyLoadingWindowPresenter(ISceneReadyObserver sceneReadyObserver, 
            ILoadingWindow loadingWindow, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _loadingWindow = loadingWindow;

            _sceneLoadService.OnSceneReload += OnSceneReload;
            _disposable = sceneReadyObserver.IsReady.Subscribe(OnSceneReady);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void OnSceneReload(Scene scene)
        {
            _loadingWindow.Open();
            _loadingWindow.ShowAsync().Forget();
        }

        private async void OnSceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }
            
            await _loadingWindow.HideAsync();
            
            _loadingWindow.Close();
        }
    }
}