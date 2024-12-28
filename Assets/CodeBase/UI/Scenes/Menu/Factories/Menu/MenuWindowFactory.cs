using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Menu;
using CodeBase.UI.Scenes.Menu.Mediators.Windows.Menu;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Factories.Menu
{
    public class MenuWindowFactory : IMenuWindowFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly IWindowCanvasProvider _windowService;

        public MenuWindowFactory(IAssetServices assetServices, IWindowCanvasProvider windowService)
        {
            _windowService = windowService;
            _assetServices = assetServices;
        }
        
        public async UniTask<MenuWindowMediator> SpawnAsync()
        {
            var canvas = await _windowService.GetCanvasAsync();
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableConstants.MenuScene.MenuWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<MenuWindowMediator>();
            
            return mediator;
        }
    }
}