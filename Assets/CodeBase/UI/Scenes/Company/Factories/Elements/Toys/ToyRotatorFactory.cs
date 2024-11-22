using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Elements.Toys;
using CodeBase.UI.Scenes.Company.Mediators.Elements.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Elements.Toys
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
