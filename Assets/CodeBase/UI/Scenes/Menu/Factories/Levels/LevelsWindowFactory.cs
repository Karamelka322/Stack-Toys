using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Levels;
using CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels;
using CodeBase.UI.Scenes.Menu.Windows.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Factories.Levels
{
    public class LevelsWindowFactory : ILevelsWindowFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly IWindowCanvasProvider _windowService;
        private readonly IMenuLevelElementFactory _menuLevelElementFactory;

        public LevelsWindowFactory(IAssetServices assetServices, IWindowCanvasProvider windowService, 
            IMenuLevelElementFactory menuLevelElementFactory)
        {
            _menuLevelElementFactory = menuLevelElementFactory;
            _windowService = windowService;
            _assetServices = assetServices;
        }

        public async UniTask<LevelsWindowMediator> SpawnAsync()
        {
            var canvas = await _windowService.GetCanvasAsync();
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.MenuScene.LevelsWindow);
            
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<LevelsWindowMediator>();
            
            
            
            return mediator;
        }
    }
}