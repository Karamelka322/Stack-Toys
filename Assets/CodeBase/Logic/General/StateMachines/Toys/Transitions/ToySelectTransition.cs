using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.Transitions
{
    public class ToySelectTransition : BaseTransition
    {
        private readonly IToyClickObserver _toyClickObserver;
        private readonly ToyMediator _toyMediator;

        public ToySelectTransition(ToyMediator toyMediator, IToyClickObserver toyClickObserver)
        {
            _toyClickObserver = toyClickObserver;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToySelectTransition> { }

        public override void Enter()
        {
            _toyClickObserver.OnClickDownAsObservableAdd(_toyMediator, OnClickDown);
        }

        public override void Exit()
        {
            _toyClickObserver.OnClickDownAsObservableRemove(_toyMediator);
        }

        private void OnClickDown()
        {
            IsCompleted.Value = true;
        }
    }
}