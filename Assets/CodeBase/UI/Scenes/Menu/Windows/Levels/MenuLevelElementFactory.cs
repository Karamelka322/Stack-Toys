using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElementFactory : IMenuLevelElementFactory
    {
        private readonly IAssetService _assetService;
        private readonly OpenedMenuLevelElement.Factory _openedMenuLevelElementFactory;
        private readonly ClosedMenuLevelElement.Factory _closedMenuLevelElementFactory;
        private readonly CompletedMenuLevelElement.Factory _completedMenuLevelElementFactory;

        public MenuLevelElementFactory(
            IAssetService assetService, 
            OpenedMenuLevelElement.Factory openedMenuLevelElementFactory,
            ClosedMenuLevelElement.Factory closedMenuLevelElementFactory,
            CompletedMenuLevelElement.Factory completedMenuLevelElementFactory)
        {
            _completedMenuLevelElementFactory = completedMenuLevelElementFactory;
            _closedMenuLevelElementFactory = closedMenuLevelElementFactory;
            _openedMenuLevelElementFactory = openedMenuLevelElementFactory;
            _assetService = assetService;
        }

        public async UniTask<CompletedMenuLevelElement> SpawnCompletedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(
                AddressableConstants.MenuScene.CompletedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();

            return _completedMenuLevelElementFactory.Create(mediator, levelIndex);
        }
        
        public async UniTask<OpenedMenuLevelElement> SpawnOpenedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(
                AddressableConstants.MenuScene.OpenedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();

            return _openedMenuLevelElementFactory.Create(mediator, levelIndex);
        }

        public async UniTask<ClosedMenuLevelElement> SpawnClosedVariantAsync(int levelIndex, Transform parent)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(
                AddressableConstants.MenuScene.ClosedLevelElement);
            
            var mediator = Object.Instantiate(prefab, parent).GetComponent<MenuLevelElementMediator>();
            
            return _closedMenuLevelElementFactory.Create(mediator, levelIndex);
        }
    }
}