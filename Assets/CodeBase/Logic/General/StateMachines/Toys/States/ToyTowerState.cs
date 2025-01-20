using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyTowerState : BaseState
    {
        private readonly ToyMediator _toyMediator;

        public ToyTowerState(ToyMediator toyMediator)
        {
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyTowerState> { }

        public override void Enter()
        {
            _toyMediator.Rigidbody.isKinematic = false;

            foreach (var collider in _toyMediator.Colliders)
            {
                collider.isTrigger = false;
            }
        }
    }
}