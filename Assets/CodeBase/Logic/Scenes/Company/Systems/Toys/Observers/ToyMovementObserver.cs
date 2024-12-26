using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
{
    public class ToyMovementObserver : IToyMovementObserver, IDisposable
    {
        private readonly IToyProvider _toyProvider;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ReactiveCollection<ToyMediator> _toysOutsideCameraFieldOfView;
        private readonly Camera _camera;

        public IReadOnlyReactiveCollection<ToyMediator> ToysOutsideCameraFieldOfView => _toysOutsideCameraFieldOfView;
        
        public ToyMovementObserver(IToyProvider toyProvider)
        {
            _toyProvider = toyProvider;
            
            _camera = Camera.main;
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
                var hasLocatedWithinCameraFieldOfView = HasLocatedWithinCameraFieldOfView(toy.Item1.transform.position);

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

        public bool HasLocatedWithinCameraFieldOfView(Vector3 position)
        {
            var viewportPoint = _camera.WorldToViewportPoint(position);
            
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                   viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                   viewportPoint.z > _camera.transform.position.z;
        }
    }
}