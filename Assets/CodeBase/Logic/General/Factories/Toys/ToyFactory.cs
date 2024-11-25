using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Toys
{
    public class ToyFactory : IToyFactory
    {
        private readonly ToyStateMachine.Factory _toyStateMachineFactory;
        private readonly ILevelsConfigProvider _levelsConfigProvider;

        public event Action<ToyMediator, ToyStateMachine> OnSpawn;
        
        public ToyFactory(ToyStateMachine.Factory toyStateMachineFactory, ILevelsConfigProvider levelsConfigProvider)
        {
            _levelsConfigProvider = levelsConfigProvider;
            _toyStateMachineFactory = toyStateMachineFactory;
        }

        public async UniTask<ToyMediator> SpawnAsync(Vector3 position)
        {
            var prefab = await _levelsConfigProvider.GetToyPrefabAsync();
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<ToyMediator>();
            
            mediator.Rigidbody.isKinematic = true;
            mediator.Collider.isTrigger = true;

            var stateMachine = _toyStateMachineFactory.Create(mediator);

            OnSpawn?.Invoke(mediator, stateMachine);
            
            return mediator;
        }
    }
}