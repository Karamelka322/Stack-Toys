using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElementFactory : IMenuLevelElementFactory
    {
        private readonly IAssetServices _assetServices;

        public MenuLevelElementFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<MenuLevelElementMediator> SpawnCompletedVariantAsync(Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.CompletedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();
            
            return mediator;
        }
        
        public async UniTask<MenuLevelElementMediator> SpawnOpenedVariantAsync(Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.OpenedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();
            
            return mediator;
        }

        public async UniTask<MenuLevelElementMediator> SpawnClosedVariantAsync(Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.ClosedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();
            
            return mediator;
        }
    }
}