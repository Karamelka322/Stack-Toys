using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
{
    public class ToyTowerObserver : IToyTowerObserver, IDisposable
    {
        private readonly IToyProvider _toyProvider;
        private readonly IDisposable _disposable;
        private readonly ILevelProvider _levelProvider;

        public ReactiveCollection<ToyMediator> Tower { get; }

        public ToyTowerObserver(IToyProvider toyProvider, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            Tower = new ReactiveCollection<ToyMediator>();
            _toyProvider = toyProvider;

            _disposable = _toyProvider.Toys.ObserveAdd().Subscribe(OnToyAdded);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            Tower?.Dispose();
        }

        private void OnToyAdded(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            addEvent.Value.Item2.SubscribeToEnterState<ToyTowerState>(
                () => OnToySet(addEvent.Value.Item1));
        }

        private void OnToySet(ToyMediator toyMediator)
        {
            var disposable = toyMediator.RigidbodyObserver.IsSleeping.Subscribe(
                isSleeping => OnSleepValueChanged(toyMediator, isSleeping));
        }

        private void OnSleepValueChanged(ToyMediator toyMediator, bool isSleeping)
        {
            if (isSleeping == false || toyMediator.RigidbodyObserver.Collisions.Count == 0)
            {
                return;
            }
            
            if (IsTowerStanding() == false)
            {
                return;
            }
            
            if (IsTowerChainPreserved() == false)
            {
                return;
            }
            
            if (IsTowerSource(toyMediator) || IsNewTowerElement(toyMediator))
            {
                Tower.Add(toyMediator);
            }
        }

        private bool IsTowerSource(ToyMediator toyMediator)
        {
            if (Tower.Count != 0) 
                return false;
            
            foreach (var collision in toyMediator.RigidbodyObserver.Collisions)
            {
                if (_levelProvider.Level.Floor.gameObject == collision.gameObject)
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

        private bool IsTowerStanding()
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