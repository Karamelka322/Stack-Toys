using System;
using System.Linq;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Systems.ToyChoicer;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class InfinityToyDestroyer : IToyDestroyer, IDisposable
    {
        private readonly IToyChoiceObserver _toyChoiceObserver;
        private readonly IToyChoicerProvider _toyChoicerProvider;
        private readonly IToyTowerBuildObserver _toyTowerBuildObserver;
        private readonly IToyProvider _toyProvider;

        public event Action OnDestroyAll;
        
        public InfinityToyDestroyer(
            IToyChoiceObserver toyChoiceObserver,
            IToyChoicerProvider toyChoicerProvider,
            IToyProvider toyProvider,
            IToyTowerBuildObserver toyTowerBuildObserver)
        {
            _toyProvider = toyProvider;
            _toyTowerBuildObserver = toyTowerBuildObserver;
            _toyChoicerProvider = toyChoicerProvider;
            _toyChoiceObserver = toyChoiceObserver;

            _toyTowerBuildObserver.OnTowerFallen += OnTowerFallen;
            _toyChoiceObserver.OnChoice += OnChoice;
        }

        public void Dispose()
        {
            _toyTowerBuildObserver.OnTowerFallen -= OnTowerFallen;
            _toyChoiceObserver.OnChoice -= OnChoice;
        }

        private void OnChoice(ToyChoicer choicer, ToyMediator toyMediator)
        {
            DestroyToyChoicer(choicer);
        }

        private void OnTowerFallen()
        {
            DestroyAll();
        }

        private void DestroyAll()
        {
            foreach (var choicer in _toyChoicerProvider.ToyChoicers.ToArray())
            {
                DestroyToyChoicer(choicer);
            }

            foreach (var toy in _toyProvider.Toys.ToArray())
            {
                DestroyToy(toy);
            }
            
            OnDestroyAll?.Invoke();
        }
        
        private void DestroyToyChoicer(ToyChoicer choicer)
        {
            _toyChoicerProvider.Unregister(choicer);
            Object.Destroy(choicer.Mediator.gameObject);
        }

        private void DestroyToy((ToyMediator, ToyStateMachine) toy)
        {
            toy.Item2.Reset();
            _toyProvider.Unregister(toy.Item1, toy.Item2);
            Object.Destroy(toy.Item1.gameObject);
        }
    }
}