using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
{
    public class ToyTowerObserver : IToyTowerObserver, IDisposable
    {
        private readonly IToySpawner _toySpawner;
        private readonly ILevelProvider _levelProvider;
        private readonly IDisposable _disposable;
        private readonly CompositeDisposable _compositeDisposable;

        private IDisposable _toySetDisposable;

        public ReactiveCollection<ToyMediator> Tower { get; }
        public event Action OnTowerFallen;

        public ToyTowerObserver(IToyProvider toyProvider, ILevelProvider levelProvider)
        {
            Tower = new ReactiveCollection<ToyMediator>();
            _compositeDisposable = new CompositeDisposable();
            
            _levelProvider = levelProvider;

            toyProvider.Toys.ObserveAdd().Subscribe(OnSpawnToy).AddTo(_compositeDisposable);
            toyProvider.Toys.ObserveRemove().Subscribe(OnRemoveToy).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _disposable?.Dispose();
            _toySetDisposable?.Dispose();
        }

        private void Reset()
        {
            _toySetDisposable?.Dispose();
            Tower.Clear();
        }

        private void OnSpawnToy(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            addEvent.Value.Item2.SubscribeToEnterState<ToyTowerState>(() => OnToySet(addEvent.Value.Item1));
        }
        
        private void OnRemoveToy(CollectionRemoveEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            if (Tower.Contains(addEvent.Value.Item1))
            {
                Reset();
            }
        }

        private void OnToySet(ToyMediator toyMediator)
        {
            _toySetDisposable = toyMediator.RigidbodyObserver.IsSleeping.Subscribe(
                isSleeping => OnSleepValueChanged(toyMediator, isSleeping));
        }

        private void OnSleepValueChanged(ToyMediator toyMediator, bool isSleeping)
        {
            if (isSleeping == false || toyMediator.RigidbodyObserver.Collisions.Count == 0)
            {
                return;
            }
            
            _toySetDisposable?.Dispose();

            if (TryAddNewTowerElement(toyMediator))
            {
                Tower.Add(toyMediator);
            }
            else
            {
                Reset();
                OnTowerFallen?.Invoke();
            }
        }

        private bool TryAddNewTowerElement(ToyMediator toyMediator)
        {
            if (CheckTowerStanding() == false)
            {
                return false;
            }

            if (IsTowerChainPreserved() == false)
            {
                return false;
            }

            if (IsTowerSource(toyMediator) || IsNewTowerElement(toyMediator))
            {
                return true;
            }

            return false;
        }

        private bool IsTowerSource(ToyMediator toyMediator)
        {
            if (Tower.Count != 0) 
                return false;
            
            foreach (var collision in toyMediator.RigidbodyObserver.Collisions)
            {
                if (_levelProvider.Level.Value.Floor.gameObject == collision.gameObject)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsNewTowerElement(ToyMediator toyMediator)
        {
            foreach (var collision in toyMediator.RigidbodyObserver.Collisions)
            {
                if (Tower.Last().gameObject == collision.gameObject)
                {
                    return IsToyAboveTower(toyMediator);
                }
            }

            return false;
        }

        private bool IsToyAboveTower(ToyMediator toyMediator)
        {
            foreach (var towerToy in Tower)
            {
                if (towerToy.transform.position.y > toyMediator.transform.position.y)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckTowerStanding()
        {
            foreach (var toyMediator in Tower)
            {
                if (toyMediator.Rigidbody.IsSleeping() == false)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsTowerChainPreserved()
        {
            for (int i = 0; i < Tower.Count - 1; i++)
            {
                if (HasToyCollision(Tower[i], Tower[i + 1]) == false)
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasToyCollision(ToyMediator firstToy, ToyMediator secondToy)
        {
            foreach (var collision in firstToy.RigidbodyObserver.Collisions)
            {
                if (collision.gameObject == secondToy.gameObject)
                {
                    return true;
                }
            }

            return false;
        }
    }
}