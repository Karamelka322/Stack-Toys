using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.General.Mediators.Windows.Pause;
using CodeBase.UI.Interfaces.General.Factories.Windows.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Factories.Windows.Pause
{
    public class PauseWindowFactory : IPauseWindowFactory
    {
        private readonly IWindowCanvasProvider _windowCanvasProvider;
        private readonly IAssetService _assetService;

        public PauseWindowFactory(IAssetService assetService, IWindowCanvasProvider windowCanvasProvider)
        {
            _assetService = assetService;
            _windowCanvasProvider = windowCanvasProvider;
        }

        public async UniTask<PauseWindowMediator> SpawnAsync()
        {
            var canvas = await _windowCanvasProvider.GetCanvasAsync();
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.PauseWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<PauseWindowMediator>();

            return mediator;
        }
    }
}