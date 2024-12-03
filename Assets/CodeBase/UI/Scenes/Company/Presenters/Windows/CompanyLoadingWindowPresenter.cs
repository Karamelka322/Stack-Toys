using System;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using UniRx;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyLoadingWindowPresenter : IDisposable
    {
        private readonly ILoadingWindow _loadingWindow;
        private readonly IDisposable _disposable;

        public CompanyLoadingWindowPresenter(ICompanySceneReady companySceneReady, ILoadingWindow loadingWindow)
        {
            _loadingWindow = loadingWindow;

            _disposable = companySceneReady.IsReady.Subscribe(OnCompanySceneReady);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnCompanySceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }
            
            _loadingWindow.Close();
        }
    }
}