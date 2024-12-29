using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.General.Factories.Canvases;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Factories.Canvases
{
    public class CanvasFactory : ICanvasFactory
    {
        private readonly IAssetService _assetService;

        public CanvasFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<Canvas> SpawnAsync(string name)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.Canvas);
            var canvas = Object.Instantiate(prefab).GetComponent<Canvas>();
            
            canvas.gameObject.name = name;
            
            return canvas;
        }
    }
}