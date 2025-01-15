using System;
using System.Threading;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Menu;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Menu.Systems.Ready
{
    public class MenuSceneReadyObserver : ISceneReadyObserver, IDisposable
    {
        private readonly IMenuSceneEnvironmentLoader _environmentLoader;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IMenuWindow _menuWindow;

        public BoolReactiveProperty IsReady { get; }

        public MenuSceneReadyObserver(IMenuSceneEnvironmentLoader environmentLoader, IMenuWindow menuWindow)
        {
            _menuWindow = menuWindow;
            _environmentLoader = environmentLoader;
            _cancellationTokenSource = new CancellationTokenSource();
            IsReady = new BoolReactiveProperty();

            WaitReadyAsync().Forget();
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            IsReady?.Dispose();
        }

        private async UniTask WaitReadyAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _environmentLoader.IsLoaded.Value == false, 
                    cancellationToken: _cancellationTokenSource.Token);
                
                await UniTask.WaitWhile(() => _menuWindow.IsShowing.Value == false, 
                    cancellationToken: _cancellationTokenSource.Token);
                
                IsReady.Value = true;
            }
            catch (OperationCanceledException e) { }
        }
    }
}