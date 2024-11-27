using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToyDestroyer : IToyDestroyer, IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IToyProvider _toyProvider;

        public event Action<ToyMediator, ToyStateMachine> OnDestroy;

        public ToyDestroyer(IToyTowerObserver toyTowerObserver, IToyProvider toyProvider)
        {
            _toyProvider = toyProvider;
            _disposable = toyTowerObserver.Tower.ObserveReset().Subscribe(OnTowerFallen);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnTowerFallen(Unit unit)
        {
            Destroy();
        }

        private void Destroy()
        {
            foreach (var toy in _toyProvider.Toys.ToArray())
            {
                toy.Item2.Reset();
                OnDestroy?.Invoke(toy.Item1, toy.Item2);
                _toyProvider.Unregister(toy.Item1, toy.Item2);
                Object.Destroy(toy.Item1.gameObject);
            }
        }
    }
}