using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Scenes.Company.Windows.Main;
using CodeBase.UI.Scenes.Infinity.Mediators.Main;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Windows.Main
{
    public class InfinityMainWindowFactory : IInfinityMainWindowFactory
    {
        private readonly IAssetService _assetService;
        private readonly IWindowCanvasProvider _windowCanvasProvider;
        private readonly ToyRotatorElement.Factory _toyRotatorElementFactory;

        public InfinityMainWindowFactory(
            IAssetService assetService,
            IWindowCanvasProvider windowCanvasProvider, 
            ToyRotatorElement.Factory toyRotatorElementFactory)
        {
            _toyRotatorElementFactory = toyRotatorElementFactory;
            _windowCanvasProvider = windowCanvasProvider;
            _assetService = assetService;
        }

        public async UniTask<InfinityMainWindowReferences> SpawnAsync()
        {
            var canvas = await _windowCanvasProvider.GetCanvasAsync();
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.InfinityScene.MainWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<InfinityMainWindowMediator>();
            var toyRotator = _toyRotatorElementFactory.Create(mediator.Slider, mediator.SliderCanvasGroup);
            
            return new InfinityMainWindowReferences()
            {
                Mediator = mediator,
                ToyRotatorElement = toyRotator
            };
        }
    }
}