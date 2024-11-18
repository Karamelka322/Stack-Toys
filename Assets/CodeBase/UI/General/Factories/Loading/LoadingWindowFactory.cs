using CodeBase.Data.Constants;
using CodeBase.UI.General.Mediators.Loading;
using CodeBase.UI.Interfaces.General.Factories.Loading;
using UnityEngine;

namespace CodeBase.UI.General.Factories.Loading
{
    public class LoadingWindowFactory : ILoadingWindowFactory
    {
        public LoadingWindowMediator Spawn()
        {
            var prefab = Resources.Load<GameObject>(ResourcesPaths.LoadingWindow);
            var mediator = Object.Instantiate(prefab).GetComponent<LoadingWindowMediator>();

            return mediator;
        }
    }
}