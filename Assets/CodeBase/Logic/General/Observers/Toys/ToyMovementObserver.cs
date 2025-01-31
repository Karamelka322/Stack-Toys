using System;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using UniRx;

namespace CodeBase.Logic.General.Observers.Toys
{
    public class ToyMovementObserver : IToyMovementObserver, IDisposable
    {
        private readonly IToyProvider _toyProvider;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly ICameraFormulas _cameraFormulas;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly ReactiveCollection<ToyMediator> _toysDroppedOutLevel;

        public IReadOnlyReactiveCollection<ToyMediator> ToysDroppedOutLevel => _toysDroppedOutLevel;
        
        public ToyMovementObserver(
            IToyProvider toyProvider,
            IToySelectObserver toySelectObserver,
            ICameraFormulas cameraFormulas)
        {
            _cameraFormulas = cameraFormulas;
            _toySelectObserver = toySelectObserver;
            _toyProvider = toyProvider;
            
            _toysDroppedOutLevel = new ReactiveCollection<ToyMediator>();
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
                
                var hasLocatedWithinCameraWidth = _cameraFormulas.
                    HasLocatedWithinCameraWidth(toy.Item1.transform.position);

                if (hasLocatedWithinCameraWidth == false && _toysDroppedOutLevel.Contains(toy.Item1) == false)
                {
                    _toysDroppedOutLevel.Add(toy.Item1);
                    return;
                }
            }
        }

        private void OnRemoveToy(CollectionRemoveEvent<(ToyMediator, ToyStateMachine)> removeEvent)
        {
            if (_toysDroppedOutLevel.Contains(removeEvent.Value.Item1))
            {
                _toysDroppedOutLevel.Remove(removeEvent.Value.Item1);
            }
        }
    }
}