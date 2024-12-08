using System;
using System.Threading;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.UI.Scenes.Company.Windows.Finish;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyFinishWindowPresenter : IDisposable
    {
        private const double Delay = 1.5d;
        
        private readonly IWindowService _windowService;
        private readonly IDisposable _disposable;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public CompanyFinishWindowPresenter(IFinishObserver finishObserver, IWindowService windowService)
        {
            _windowService = windowService;
            _cancellationTokenSource = new CancellationTokenSource();
            
            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }
        
        private async void OnFinishValueChanged(bool isFinish)
        {
            if (isFinish == false)
            {
                return;
            }
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Delay), cancellationToken: _cancellationTokenSource.Token);
                
                _windowService.OpenAsync<CompanyFinishWindow>().Forget();
            }
            catch (OperationCanceledException e) { }
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}