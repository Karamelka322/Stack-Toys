using System;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.StateMachines.Toys.Transitions;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.ToyChoicer
{
    public class ToyChoicer : IDisposable
    {
        private readonly ToyChoicerMediator _choicerMediator;
        private readonly ToyMediator _toy1;
        private readonly ToyMediator _toy2;
        
        private readonly IToyChoicerRotateAnimation _toyChoicerRotateAnimation;
        private readonly IToyRotateAnimation _toyRotateAnimation;
        private readonly IToyClickObserver _toyClickObserver;
        private readonly ToyStateMachine2.Factory _toyStateMachineFactory;
        private readonly IToyProvider _toyProvider;
        private readonly IToyBabbleSystem _toyBabbleSystem;

        public ToyChoicerMediator Mediator => _choicerMediator;
        
        public event Action<ToyChoicer, ToyMediator> OnChoice;

        public ToyChoicer(
            ToyChoicerMediator choicerMediator,
            ToyMediator toy1,
            ToyMediator toy2,
            ToyStateMachine2.Factory toyStateMachineFactory,
            IToyProvider toyProvider,
            IToyBabbleSystem toyBabbleSystem,
            IToyChoicerRotateAnimation toyChoicerRotateAnimation,
            IToyRotateAnimation toyRotateAnimation,
            IToyClickObserver toyClickObserver)
        {
            _toyBabbleSystem = toyBabbleSystem;
            _toyStateMachineFactory = toyStateMachineFactory;
            _toyProvider = toyProvider;
            _toyClickObserver = toyClickObserver;
            _toyRotateAnimation = toyRotateAnimation;
            _toyChoicerRotateAnimation = toyChoicerRotateAnimation;
            _choicerMediator = choicerMediator;
            _toy1 = toy1;
            _toy2 = toy2;
            
            _toyChoicerRotateAnimation.Play(_choicerMediator);
            _toyRotateAnimation.Play(_toy1);
            _toyRotateAnimation.Play(_toy2);
            
            _toyClickObserver.OnClickDownAsObservableAdd(_toy1, OnChoiceToy1);
            _toyClickObserver.OnClickDownAsObservableAdd(_toy2, OnChoiceToy2);
        }
        
        public class Factory : PlaceholderFactory<ToyChoicerMediator, ToyMediator, ToyMediator, ToyChoicer> { }

        public void Dispose()
        {
            _toyChoicerRotateAnimation.Stop(_choicerMediator);
            _toyRotateAnimation.Stop(_toy1);
            _toyRotateAnimation.Stop(_toy2);
            
            _toyClickObserver.OnClickDownAsObservableRemove(_toy1);
            _toyClickObserver.OnClickDownAsObservableRemove(_toy2);
        }
        
        private void OnChoiceToy1()
        {
            Dispose();
            Choice(_toy1);
        }

        private void OnChoiceToy2()
        {
            Dispose();
            Choice(_toy2);
        }

        private void Choice(ToyMediator toyMediator)
        {
            toyMediator.transform.parent = null;
            
            _toyBabbleSystem.Remove(_toy1);
            _toyBabbleSystem.Remove(_toy2);

            var stateMachine = _toyStateMachineFactory.Create(toyMediator);
            _toyProvider.Register(toyMediator, stateMachine);

            OnChoice?.Invoke(this, toyMediator);
        }
    }
}