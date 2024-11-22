using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
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
            addEvent.Value.Item2.SubscribeToEnterState<ToyRotateState>(
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