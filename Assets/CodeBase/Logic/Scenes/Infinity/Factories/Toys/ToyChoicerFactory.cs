using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
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
        private readonly ToyChoicerStateMachine.Factory _toyChoicerStateMachineFactory;
        private readonly IToyBabbleSystem _toyBabbleSystem;

        public ToyChoicerFactory(
            IAssetService assetService,
            IToyBabbleSystem toyBabbleSystem,
            ToyChoicerStateMachine.Factory toyChoicerStateMachineFactory)
        {
            _toyBabbleSystem = toyBabbleSystem;
            _toyChoicerStateMachineFactory = toyChoicerStateMachineFactory;
            _assetService = assetService;
        }

        public async UniTask<(ToyChoicerMediator, ToyChoicerStateMachine)> SpawnAsync(AssetReferenceGameObject toyAsset1, 
            AssetReferenceGameObject toyAsset2, Vector3 position)
        {
            var choicerKey = AddressableConstants.InfinityScene.ToyChoicer;
            var mediator = await SpawnAsync<ToyChoicerMediator>(choicerKey, null, position);
            
            var toy1 = await SpawnToyAsync(toyAsset1.AssetGUID, mediator.ToySlot1, mediator.ToySlot1.position);
            var toy2 = await SpawnToyAsync(toyAsset2.AssetGUID, mediator.ToySlot2, mediator.ToySlot2.position);
            
            var stateMachine = _toyChoicerStateMachineFactory.Create(mediator, toy1, toy2);
            
            stateMachine.Launch();
            
            return (mediator, stateMachine);
        }

        private async UniTask<ToyMediator> SpawnToyAsync(string addressable, Transform parent, Vector3 position)
        {
            var toy = await SpawnAsync<ToyMediator>(addressable, parent, position);

            toy.Rigidbody.isKinematic = true;
            await _toyBabbleSystem.AddAsync(toy);

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