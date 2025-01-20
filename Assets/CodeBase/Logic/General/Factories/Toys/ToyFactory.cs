using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Toys
{
    public class ToyFactory : IToyFactory
    {
        private readonly ToyStateMachine.Factory _toyStateMachineFactory;
        private readonly IAssetService _assetService;

        public ToyFactory(ToyStateMachine.Factory toyStateMachineFactory, IAssetService assetService)
        {
            _assetService = assetService;
            _toyStateMachineFactory = toyStateMachineFactory;
        }

        public (ToyMediator, ToyStateMachine) Spawn(GameObject prefab, Transform parent, Vector3 position)
        {
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<ToyMediator>();
            
            mediator.Rigidbody.isKinematic = true;

            foreach (var collider in mediator.Colliders)
            {
                collider.isTrigger = true;
            }

            var stateMachine = _toyStateMachineFactory.Create(mediator);

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