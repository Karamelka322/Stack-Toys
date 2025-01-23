using System;
using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Observers.Toys
{
    public class ToyChoiceObserver : IToyChoiceObserver, IDisposable
    {
        private readonly IToyChoicerProvider _toyChoicerProvider;
        private readonly CompositeDisposable _compositeDisposable;

        public event Action<ToyChoicer, ToyMediator> OnChoice;
        
        public ToyChoiceObserver(IToyChoicerProvider toyChoicerProvider)
        {
            _toyChoicerProvider = toyChoicerProvider;

            _compositeDisposable = new CompositeDisposable();
            
            _toyChoicerProvider.ToyChoicers.ObserveAdd().Subscribe(OnAddToyChoicer).AddTo(_compositeDisposable);
            _toyChoicerProvider.ToyChoicers.ObserveRemove().Subscribe(OnRemoveToyChoicer).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();

            foreach (var choicer in _toyChoicerProvider.ToyChoicers)
            {
                choicer.OnChoice -= OnChoiceInvoker;
            }
        }

        private void OnAddToyChoicer(CollectionAddEvent<ToyChoicer> addEvent)
        {
            addEvent.Value.OnChoice += OnChoiceInvoker;
        }
        
        private void OnRemoveToyChoicer(CollectionRemoveEvent<ToyChoicer> removeEvent)
        {
            removeEvent.Value.OnChoice -= OnChoiceInvoker;
        }

        private void OnChoiceInvoker(ToyChoicer toyChoicer, ToyMediator toyMediator)
        {
            OnChoice?.Invoke(toyChoicer, toyMediator);
        }
    }
}