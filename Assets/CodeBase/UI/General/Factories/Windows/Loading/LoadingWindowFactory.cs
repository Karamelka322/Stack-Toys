using CodeBase.Data.Constants;
using CodeBase.UI.General.Mediators.Windows.Loading;
using CodeBase.UI.Interfaces.General.Factories.Windows.Loading;
using UnityEngine;

namespace CodeBase.UI.General.Factories.Windows.Loading
{
    public class LoadingWindowFactory : ILoadingWindowFactory
    {
        public LoadingWindowMediator Spawn()
        {
            var prefab = Resources.Load<GameObject>(ResourcesPaths.LoadingWindow);
            var mediator = Object.Instantiate(prefab).GetComponent<LoadingWindowMediator>();
            
            Object.DontDestroyOnLoad(mediator.gameObject);
            
            return mediator;
        }
    }
}