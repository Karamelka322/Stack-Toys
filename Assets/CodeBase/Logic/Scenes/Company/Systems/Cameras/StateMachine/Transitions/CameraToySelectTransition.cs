using System;
using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using UniRx;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.Transitions
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