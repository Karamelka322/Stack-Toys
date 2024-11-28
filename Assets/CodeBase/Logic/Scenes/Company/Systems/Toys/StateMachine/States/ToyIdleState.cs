using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
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