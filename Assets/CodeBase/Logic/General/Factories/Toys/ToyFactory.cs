using System;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Services.Assets;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Toys
{
    public class ToyFactory : IToyFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly ToyStateMachine.Factory _toyStateMachineFactory;

        public event Action<ToyMediator, ToyStateMachine> OnSpawn;
        
        public ToyFactory(IAssetServices assetServices, ToyStateMachine.Factory toyStateMachineFactory)
        {
            _toyStateMachineFactory = toyStateMachineFactory;
            _assetServices = assetServices;
        }

        public async UniTask<ToyMediator> SpawnAsync(Vector3 position)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.Toy);
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<ToyMediator>();

            var stateMachine = _toyStateMachineFactory.Create(mediator);

            OnSpawn?.Invoke(mediator, stateMachine);
            
            return mediator;
        }
    }
}