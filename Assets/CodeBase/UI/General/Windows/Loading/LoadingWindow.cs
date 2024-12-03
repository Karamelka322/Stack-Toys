using CodeBase.UI.General.Mediators.Windows.Loading;
using CodeBase.UI.Interfaces.General.Factories.Windows.Loading;
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
            if (_window == null)
            {
                return;
            }
            
            Object.Destroy(_window.gameObject);
        }
    }
}