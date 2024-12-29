using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
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
        private readonly IAssetService _assetService;
        private readonly IWindowCanvasProvider _windowService;
        private readonly IMenuLevelElementFactory _menuLevelElementFactory;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

        public LevelsWindowFactory(
            IAssetService assetService,
            IWindowCanvasProvider windowService, 
            IMenuLevelElementFactory menuLevelElementFactory,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _menuLevelElementFactory = menuLevelElementFactory;
            _windowService = windowService;
            _assetService = assetService;
        }

        public async UniTask<LevelsWindowMediator> SpawnAsync()
        {
            var canvas = await _windowService.GetCanvasAsync();
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.MenuScene.LevelsWindow);
            
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<LevelsWindowMediator>();
            
            for (int i = 0; i < CompanyConstants.NumberOfLevels; i++)
            {
                if (_companyLevelsSaveDataProvider.HasClosedLevel(i))
                {
                    await _menuLevelElementFactory.SpawnClosedVariantAsync(i, mediator.LevelsParent);
                }
                else if(_companyLevelsSaveDataProvider.HasOpenedLevel(i))
                {
                    await _menuLevelElementFactory.SpawnOpenedVariantAsync(i, mediator.LevelsParent);
                }
                else if (_companyLevelsSaveDataProvider.HasCompletedLevel(i))
                {
                    await _menuLevelElementFactory.SpawnCompletedVariantAsync(i, mediator.LevelsParent);
                }
            }
            
            return mediator;
        }
    }
}