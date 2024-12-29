using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Main;
using CodeBase.UI.Scenes.Company.RuntimeData;
using CodeBase.UI.Scenes.Company.Windows.Main;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Windows.Main
{
    public class CompanyMainWindowFactory : ICompanyMainWindowFactory
    {
        private readonly IAssetService _assetService;
        private readonly IWindowCanvasProvider _windowCanvasProvider;
        private readonly ToyRotatorElement.Factory _toyRotatorElementFactory;

        public CompanyMainWindowFactory(
            IAssetService assetService,
            IWindowCanvasProvider windowCanvasProvider, 
            ToyRotatorElement.Factory toyRotatorElementFactory)
        {
            _toyRotatorElementFactory = toyRotatorElementFactory;
            _windowCanvasProvider = windowCanvasProvider;
            _assetService = assetService;
        }

        public async UniTask<CompanyMainWindowReferences> SpawnAsync()
        {
            var canvas = await _windowCanvasProvider.GetCanvasAsync();
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.CompanyScene.MainWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<CompanyMainWindowMediator>();
            var toyRotator = _toyRotatorElementFactory.Create(mediator.Slider, mediator.SliderCanvasGroup);
            
            return new CompanyMainWindowReferences()
            {
                Mediator = mediator,
                ToyRotatorElement = toyRotator
            };
        }
    }
}