using UnityEngine;

namespace CodeBase.UI.Windows.Loading
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