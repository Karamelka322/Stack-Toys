using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElementFactory : IMenuLevelElementFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly OpenedMenuLevelElement.Factory _openedMenuLevelElementFactory;
        private readonly ClosedMenuLevelElement.Factory _closedMenuLevelElementFactory;
        private readonly CompletedMenuLevelElement.Factory _completedMenuLevelElementFactory;

        public MenuLevelElementFactory(
            IAssetServices assetServices, 
            OpenedMenuLevelElement.Factory openedMenuLevelElementFactory,
            ClosedMenuLevelElement.Factory closedMenuLevelElementFactory,
            CompletedMenuLevelElement.Factory completedMenuLevelElementFactory)
        {
            _completedMenuLevelElementFactory = completedMenuLevelElementFactory;
            _closedMenuLevelElementFactory = closedMenuLevelElementFactory;
            _openedMenuLevelElementFactory = openedMenuLevelElementFactory;
            _assetServices = assetServices;
        }

        public async UniTask<CompletedMenuLevelElement> SpawnCompletedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.CompletedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();

            return _completedMenuLevelElementFactory.Create(mediator, levelIndex);
        }
        
        public async UniTask<OpenedMenuLevelElement> SpawnOpenedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.OpenedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();

            return _openedMenuLevelElementFactory.Create(mediator, levelIndex);
        }

        public async UniTask<ClosedMenuLevelElement> SpawnClosedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.MenuScene.ClosedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();
            
            return _closedMenuLevelElementFactory.Create(mediator, levelIndex);
        }
    }
}