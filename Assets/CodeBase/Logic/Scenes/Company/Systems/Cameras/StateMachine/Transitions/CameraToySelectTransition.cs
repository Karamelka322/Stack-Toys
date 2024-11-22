using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using UniRx;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.Transitions
{
    public class CameraToySelectTransition : BaseTransition
    {
        private readonly IToySelectObserver _toySelectObserver;
        private IDisposable _disposable;

        public CameraToySelectTransition(IToySelectObserver toySelectObserver)
        {
            _toySelectObserver = toySelectObserver;
        }
        
        public class Factory : PlaceholderFactory<CameraToySelectTransition> { }

        public override void Enter()
        {
            _disposable = _toySelectObserver.Toy.Subscribe(OnToySelect);
        }

        public override void Exit()
        {
            _disposable?.Dispose();
        }

        private void OnToySelect(ToyMediator toy)
        {
            if (toy == null)
            {
                return;
            }

            IsCompleted.Value = true;
        }
    }
}