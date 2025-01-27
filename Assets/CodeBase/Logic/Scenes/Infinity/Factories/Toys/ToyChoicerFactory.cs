using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Systems.ToyChoicer;
using CodeBase.Logic.General.Systems.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Scenes.Infinity.Factories.Toys
{
    public class ToyChoicerFactory : IToyChoicerFactory
    {
        private readonly IAssetService _assetService;
        private readonly IToyBabbleSystem _toyBabbleSystem;
        private readonly ToyChoicer.Factory _toyChoicerFactory;
        private readonly IToyShadowSystem _toyShadowSystem;

        public ToyChoicerFactory(
            ToyChoicer.Factory _toyChoicerFactory,
            IAssetService assetService,
            IToyShadowSystem toyShadowSystem,
            IToyBabbleSystem toyBabbleSystem)
        {
            _toyShadowSystem = toyShadowSystem;
            this._toyChoicerFactory = _toyChoicerFactory;
            _toyBabbleSystem = toyBabbleSystem;
            _assetService = assetService;
        }

        public async UniTask<ToyChoicer> SpawnAsync(AssetReferenceGameObject toyAsset1, 
            AssetReferenceGameObject toyAsset2, Vector3 position)
        {
            var choicerKey = AddressableConstants.InfinityScene.ToyChoicer;
            var choicerPrefab = await _assetService.LoadAsync<GameObject>(choicerKey);
            
            var toyPrefab1 = await _assetService.LoadAsync<GameObject>(toyAsset1.AssetGUID);
            var toyPrefab2 = await _assetService.LoadAsync<GameObject>(toyAsset2.AssetGUID);
            
            var choicerMediator = Object.Instantiate(choicerPrefab, position,
                Quaternion.identity).GetComponent<ToyChoicerMediator>();
            
            var toy1 = await SpawnToyAsync(toyPrefab1, choicerMediator.ToySlot1);
            var toy2 = await SpawnToyAsync(toyPrefab2, choicerMediator.ToySlot2);
            
            return _toyChoicerFactory.Create(choicerMediator, toy1, toy2);
        }

        private async UniTask<ToyMediator> SpawnToyAsync(GameObject prefab, Transform parent)
        {
            var toy = Object.Instantiate(prefab, parent).GetComponent<ToyMediator>();

            await _toyShadowSystem.AddAsync(toy);
            toy.Rigidbody.isKinematic = true;
            await _toyBabbleSystem.AddAsync(toy);
            
            foreach (var collider in toy.Colliders)
            {
                collider.isTrigger = true;
            }

            return toy;
        }
    }
}