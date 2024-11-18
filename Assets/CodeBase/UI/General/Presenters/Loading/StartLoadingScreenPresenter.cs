using CodeBase.UI.Windows.Loading;

namespace CodeBase.UI.Scenes.Bootstrap.Presenters
{
    public class StartLoadingScreenPresenter
    {
        public StartLoadingScreenPresenter(ILoadingWindow loadingWindow)
        {
            loadingWindow.Open();
        }
    }
}