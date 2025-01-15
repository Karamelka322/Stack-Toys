using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Menu.Systems
{
    public class MenuLoadingScreenPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly ILoadingWindow _loadingWindow;
        private readonly ISceneLoadService _sceneLoadService;

        public MenuLoadingScreenPresenter(ISceneReadyObserver sceneReadyObserver, 
            ILoadingWindow loadingWindow, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _loadingWindow = loadingWindow;

            _sceneLoadService.OnSceneLoading += OnSceneLoading;
            _disposable = sceneReadyObserver.IsReady.Subscribe(OnSceneReady);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
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

        private void OnSceneLoading(Scene scene)
        {
            _loadingWindow.Open();
            _loadingWindow.ShowAsync().Forget();
        }
    }
}