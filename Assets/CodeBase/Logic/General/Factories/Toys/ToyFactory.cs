using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
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
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

        public event Action<ToyMediator, ToyStateMachine> OnSpawn;
        
        public ToyFactory(
            ToyStateMachine.Factory toyStateMachineFactory,
            ILevelsConfigProvider levelsConfigProvider, 
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelsConfigProvider = levelsConfigProvider;
            _toyStateMachineFactory = toyStateMachineFactory;
        }

        public async UniTask<ToyMediator> SpawnAsync(Vector3 position)
        {
            var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var prefabs = await _levelsConfigProvider.GetToyPrefabsAsync(currentLevel);
            var mediator = Object.Instantiate(prefabs[0], position, Quaternion.identity).GetComponent<ToyMediator>();
            
            mediator.Rigidbody.isKinematic = true;
            mediator.Collider.isTrigger = true;

            var stateMachine = _toyStateMachineFactory.Create(mediator);

            OnSpawn?.Invoke(mediator, stateMachine);
            
            return mediator;
        }
    }
}