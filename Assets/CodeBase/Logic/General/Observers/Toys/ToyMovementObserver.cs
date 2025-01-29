using System;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Observers.Toys
{
    public class ToyMovementObserver : IToyMovementObserver, IDisposable
    {
        private readonly IToyProvider _toyProvider;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ReactiveCollection<ToyMediator> _toysOutsideCameraFieldOfView;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly ICameraFormulas _cameraFormulas;

        public IReadOnlyReactiveCollection<ToyMediator> ToysOutsideCameraFieldOfView => _toysOutsideCameraFieldOfView;
        
        public ToyMovementObserver(
            IToyProvider toyProvider,
            IToySelectObserver toySelectObserver,
            ICameraFormulas cameraFormulas)
        {
            _cameraFormulas = cameraFormulas;
            _toySelectObserver = toySelectObserver;
            _toyProvider = toyProvider;
            
            _toysOutsideCameraFieldOfView = new ReactiveCollection<ToyMediator>();
            _compositeDisposable = new CompositeDisposable();

            _toyProvider.Toys.ObserveRemove().Subscribe(OnRemoveToy).AddTo(_compositeDisposable);
            Observable.EveryUpdate().Subscribe(OnUpdate).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnUpdate(long tick)
        {
            foreach (var toy in _toyProvider.Toys)
            {
                if (_toySelectObserver.Toy.Value == toy.Item1)
                {
                    continue;
                }
                
                var hasLocatedWithinCameraFieldOfView = _cameraFormulas.
                    HasLocatedWithinCameraFieldOfView(toy.Item1.transform.position);

                if (hasLocatedWithinCameraFieldOfView == false &&
                    _toysOutsideCameraFieldOfView.Contains(toy.Item1) == false)
                {
                    _toysOutsideCameraFieldOfView.Add(toy.Item1);
                    return;
                }
            }
        }

        private void OnRemoveToy(CollectionRemoveEvent<(ToyMediator, ToyStateMachine)> removeEvent)
        {
            if (_toysOutsideCameraFieldOfView.Contains(removeEvent.Value.Item1))
            {
                _toysOutsideCameraFieldOfView.Remove(removeEvent.Value.Item1);
            }
        }
    }
}