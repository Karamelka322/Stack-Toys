using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.Services.Assets;
using CodeBase.Logic.Services.Window;
using CodeBase.UI.Scenes.Company.Elements.Toys.Rotator;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Elements.Toys.Rotator
{
    public class ToyRotatorFactory : IToyRotatorFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly IGameCanvasProvider _gameCanvasProvider;

        public ToyRotatorFactory(IAssetServices assetServices, IGameCanvasProvider gameCanvasProvider)
        {
            _gameCanvasProvider = gameCanvasProvider;
            _assetServices = assetServices;
        }

        public async UniTask<ToyRotatorMediator> SpawnAsync(Transform target)
        {
            var canvas = await _gameCanvasProvider.GetCanvasAsync();
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.CompanyScene.ToyRotator);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<ToyRotatorMediator>();
            
            return mediator;
        }
    }
}
