using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyIdleState : BaseState
    {
        private readonly ToyMediator _toyMediator;

        public ToyIdleState(ToyMediator toyMediator)
        {
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyIdleState> { }
    }
}