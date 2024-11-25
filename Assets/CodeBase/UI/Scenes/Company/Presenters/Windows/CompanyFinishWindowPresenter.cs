using System;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Finish;
using CodeBase.UI.Scenes.Company.Windows.Finish;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyFinishWindowPresenter : IDisposable
    {
        private readonly IWindowService _windowService;
        private readonly IDisposable _disposable;

        public CompanyFinishWindowPresenter(IFinishObserver finishObserver, IWindowService windowService)
        {
            _windowService = windowService;
            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }
        
        private void OnFinishValueChanged(bool isFinish)
        {
            if (isFinish == false)
            {
                return;
            }
            
            _windowService.OpenAsync<CompanyFinishWindow>().Forget();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}