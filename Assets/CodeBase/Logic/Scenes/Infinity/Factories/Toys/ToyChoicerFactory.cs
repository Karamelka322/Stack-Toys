using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Scenes.Infinity.Factories.Toys
{
    public class ToyChoicerFactory : IToyChoicerFactory
    {
        private readonly IAssetService _assetService;
        private readonly IToyFactory _toyFactory;
        private readonly IToySelectObserver _toySelectObserver;

        public ToyChoicerFactory(IAssetService assetService, IToyFactory toyFactory, IToySelectObserver toySelectObserver)
        {
            _toySelectObserver = toySelectObserver;
            _toyFactory = toyFactory;
            _assetService = assetService;
        }

        public async UniTask<ToyChoicer> SpawnAsync(AssetReferenceGameObject toyAsset1, 
            AssetReferenceGameObject toyAsset2, Vector3 position)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.InfinityScene.ToyChoicer);
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<ToyChoicerMediator>();

            var toy1 = await _toyFactory.SpawnAsync(toyAsset1.AssetGUID,
                mediator.ToySlot1, mediator.ToySlot1.position);
            
            var toy2 = await _toyFactory.SpawnAsync(toyAsset2.AssetGUID,
                mediator.ToySlot2, mediator.ToySlot2.position);
            
            return new ToyChoicer(toy1.Item1, toy2.Item1, _toySelectObserver);
        }
    }
}