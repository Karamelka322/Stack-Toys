using CodeBase.UI.Interfaces.General.Windows.Loading;

namespace CodeBase.UI.General.Presenters.Loading
{
    public class StartLoadingScreenPresenter
    {
        public StartLoadingScreenPresenter(ILoadingWindow loadingWindow)
        {
            loadingWindow.Open();
        }
    }
}