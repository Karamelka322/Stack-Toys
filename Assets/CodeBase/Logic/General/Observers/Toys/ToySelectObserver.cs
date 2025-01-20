using System;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using UniRx;

namespace CodeBase.Logic.General.Observers.Toys
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
            addEvent.Value.Item2.SubscribeToEnterState<ToyIdleState>(
                () => OnToySelect(addEvent.Value.Item1));
            
            addEvent.Value.Item2.SubscribeToEnterState<ToyTowerState>(
                () => OnToySet(addEvent.Value.Item1));
        }
        
        private void OnToySelect(ToyMediator toyMediator)
        {
            Toy.Value = toyMediator;
        }

        private void OnToySet(ToyMediator toyMediator)
        {
            if (toyMediator == Toy.Value)
            {
                Toy.Value = null;
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            Toy?.Dispose();
        }
    }
}