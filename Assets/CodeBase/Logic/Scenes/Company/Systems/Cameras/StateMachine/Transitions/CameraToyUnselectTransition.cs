using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using UniRx;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.Transitions
{
    public class CameraToyUnselectTransition : BaseTransition
    {
        private readonly IToySelectObserver _toySelectObserver;
        private IDisposable _disposable;

        private ToyMediator _toySelected;

        public CameraToyUnselectTransition(IToySelectObserver toySelectObserver)
        {
            _toySelectObserver = toySelectObserver;
        }
        
        public class Factory : PlaceholderFactory<CameraToyUnselectTransition> { }

        public override void Enter()
        {
            _toySelected = _toySelectObserver.Toy.Value;
            _disposable = _toySelectObserver.Toy.Subscribe(OnToySelect);
        }

        public override void Exit()
        {
            _disposable?.Dispose();
        }

        private void OnToySelect(ToyMediator toy)
        {
            if (_toySelected != null && toy == null)
            {
                IsCompleted.Value = true;
            }
        }
    }
}