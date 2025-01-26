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
            var mediator = await SpawnAsync<ToyChoicerMediator>(choicerKey, null, position);
            
            var toy1 = await SpawnToyAsync(toyAsset1.AssetGUID, mediator.ToySlot1, mediator.ToySlot1.position);
            var toy2 = await SpawnToyAsync(toyAsset2.AssetGUID, mediator.ToySlot2, mediator.ToySlot2.position);
            
            return _toyChoicerFactory.Create(mediator, toy1, toy2);
        }

        private async UniTask<ToyMediator> SpawnToyAsync(string addressable, Transform parent, Vector3 position)
        {
            var toy = await SpawnAsync<ToyMediator>(addressable, parent, position);

            await _toyShadowSystem.AddAsync(toy);
            toy.Rigidbody.isKinematic = true;
            await _toyBabbleSystem.AddAsync(toy);
            
            foreach (var collider in toy.Colliders)
            {
                collider.isTrigger = true;
            }

            return toy;
        }
        
        private async UniTask<TComponent> SpawnAsync<TComponent>(string addressable,
            Transform parent, Vector3 position) where TComponent : MonoBehaviour
        {
            var prefab = await _assetService.LoadAsync<GameObject>(addressable);
            var component = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<TComponent>();
            
            return component;
        }
    }
}