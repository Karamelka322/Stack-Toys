using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Systems.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Toys
{
    public class ToyFactory : IToyFactory
    {
        private readonly ToyStateMachine.Factory _toyStateMachineFactory;
        private readonly IAssetService _assetService;
        private readonly IToyShadowSystem _toyShadowSystem;

        public ToyFactory(ToyStateMachine.Factory toyStateMachineFactory, IAssetService assetService, IToyShadowSystem toyShadowSystem)
        {
            _toyShadowSystem = toyShadowSystem;
            _assetService = assetService;
            _toyStateMachineFactory = toyStateMachineFactory;
        }

        public (ToyMediator, ToyStateMachine) Spawn(GameObject prefab, Transform parent, Vector3 position)
        {
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<ToyMediator>();

            _toyShadowSystem.AddAsync(mediator).Forget();
            mediator.Rigidbody.isKinematic = true;

            foreach (var collider in mediator.Colliders)
            {
                collider.isTrigger = true;
            }

            var stateMachine = _toyStateMachineFactory.Create(mediator);
            stateMachine.Launch();
            return (mediator, stateMachine);
        }

        public async UniTask<(ToyMediator, ToyStateMachine)> SpawnAsync(string addressableName, 
            Transform parent, Vector3 position)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);
            return Spawn(prefab, parent, position);
        }
    }
}