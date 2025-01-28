using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
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
        private readonly IAssetService _assetService;
        private readonly IWindowCanvasProvider _windowService;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

        public MenuWindowFactory(
            IAssetService assetService,
            IWindowCanvasProvider windowService,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _windowService = windowService;
            _assetService = assetService;
        }
        
        public async UniTask<MenuWindowMediator> SpawnAsync()
        {
            var canvas = await _windowService.GetCanvasAsync();
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.MenuScene.MenuWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<MenuWindowMediator>();

            var lastOpenedLevel = _companyLevelsSaveDataProvider.GetLastOpenedLevel() + 1;
            
            if (lastOpenedLevel >= InfinitySceneConstants.CompanyLevelForOpenInfinityMode)
            {
                mediator.ClosedInfinityModeButton.gameObject.SetActive(false);
            }
            else
            {
                mediator.InfinityModeButton.gameObject.SetActive(false);
                
                mediator.LevelsCounter.text = InfinitySceneConstants.CompanyLevelForOpenInfinityMode.ToString();
            }
            
            return mediator;
        }
    }
}