using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.General.Factories.Canvases;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Factories.Canvases
{
    public class CanvasFactory : ICanvasFactory
    {
        private readonly IAssetServices _assetServices;

        public CanvasFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<Canvas> SpawnAsync(string name)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableConstants.Canvas);
            var canvas = Object.Instantiate(prefab).GetComponent<Canvas>();
            
            canvas.gameObject.name = name;
            
            return canvas;
        }
    }
}