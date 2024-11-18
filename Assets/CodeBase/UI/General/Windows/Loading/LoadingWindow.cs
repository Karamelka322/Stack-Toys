using CodeBase.UI.General.Mediators.Loading;
using CodeBase.UI.Interfaces.General.Factories.Loading;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using UnityEngine;

namespace CodeBase.UI.General.Windows.Loading
{
    public class LoadingWindow : ILoadingWindow
    {
        private readonly ILoadingWindowFactory _loadingWindowFactory;
        
        private LoadingWindowMediator _window;

        public LoadingWindow(ILoadingWindowFactory loadingWindowFactory)
        {
            _loadingWindowFactory = loadingWindowFactory;
        }

        public void Open()
        {
            _window = _loadingWindowFactory.Spawn();
        }

        public void Close()
        {
            Object.Destroy(_window);
        }
    }
}