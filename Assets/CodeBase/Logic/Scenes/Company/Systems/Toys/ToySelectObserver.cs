using System;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToySelectObserver : IToySelectObserver, IDisposable
    {
        private readonly IDisposable _disposable;

        public ReactiveProperty<ToyMediator> Toy { get; }

        public ToySelectObserver(IToyProvider toyProvider)
        {
            Toy = new ReactiveProperty<ToyMediator>();
            
            _disposable = toyProvider.Toys.ObserveAdd().Subscribe(OnToyAdd);
        }
        
        private void OnToyAdd(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            addEvent.Value.Item2.SubscribeToExitState<ToyBabbleState>(() => OnToyReady(addEvent.Value.Item1));
        }
        
        private void OnToyReady(ToyMediator toyMediator)
        {
            Toy.Value = toyMediator;
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
            Toy?.Dispose();
        }
    }
}