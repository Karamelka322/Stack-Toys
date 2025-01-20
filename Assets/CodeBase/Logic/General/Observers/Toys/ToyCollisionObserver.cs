using System;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Observers.Toys
{
    public interface IToyCollisionObserver
    {
        event Action<GameObject> OnCollision;
    }

    public class ToyCollisionObserver : IToyCollisionObserver, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable;
        
        public event Action<GameObject> OnCollision;
        
        public ToyCollisionObserver(IToyProvider toyProvider)
        {
            _compositeDisposable = new CompositeDisposable();
            
            toyProvider.Toys.ObserveAdd().Subscribe(OnAddToy).AddTo(_compositeDisposable);
        }
        
        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        private void OnAddToy(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            addEvent.Value.Item1.RigidbodyObserver.Collisions.ObserveAdd()
                .Subscribe(OnAddCollision).AddTo(_compositeDisposable);
        }
        
        private void OnAddCollision(CollectionAddEvent<GameObject> addEvent)
        {
            OnCollision?.Invoke(addEvent.Value);
        }
    }
}