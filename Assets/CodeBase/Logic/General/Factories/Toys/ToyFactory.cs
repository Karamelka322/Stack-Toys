using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Toys
{
    public class ToyFactory : IToyFactory
    {
        private readonly ToyStateMachine.Factory _toyStateMachineFactory;
        
        public ToyFactory(ToyStateMachine.Factory toyStateMachineFactory)
        {
            _toyStateMachineFactory = toyStateMachineFactory;
        }

        public (ToyMediator, ToyStateMachine) Spawn(GameObject prefab, Vector3 position)
        {
            var mediator = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<ToyMediator>();
            
            mediator.Rigidbody.isKinematic = true;
            mediator.Collider.isTrigger = true;

            var stateMachine = _toyStateMachineFactory.Create(mediator);

            return (mediator, stateMachine);
        }
    }
}