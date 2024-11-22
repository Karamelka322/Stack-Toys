using System;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Finish;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyFinishWindowPresenter : IDisposable
    {
        private readonly ICompanyFinishWindow _companyFinishWindow;
        private readonly IDisposable _disposable;

        public CompanyFinishWindowPresenter(IFinishObserver finishObserver, ICompanyFinishWindow companyFinishWindow)
        {
            _companyFinishWindow = companyFinishWindow;
            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }

        private void OnFinishValueChanged(bool isFinish)
        {
            if (isFinish == false)
            {
                return;
            }
		    
            _companyFinishWindow.OpenAsync().Forget();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}