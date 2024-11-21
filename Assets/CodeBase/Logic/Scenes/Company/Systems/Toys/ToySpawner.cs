using System;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Providers;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToySpawner : IDisposable
    {
        private readonly IToyTowerObserver _toyTowerObserver;
        private readonly IToyFactory _toyFactory;
        private readonly IDisposable _disposable;
        private readonly ILevelProvider _levelProvider;

        public ToySpawner(IToyTowerObserver toyTowerObserver, IToyFactory toyFactory, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _toyFactory = toyFactory;
            _toyTowerObserver = toyTowerObserver;

            _disposable = _toyTowerObserver.Tower.ObserveAdd().Subscribe(OnIncreaseToyTower);
        }

        private void OnIncreaseToyTower(CollectionAddEvent<ToyMediator> addEvent)
        {
            _toyFactory.SpawnAsync(_levelProvider.Level.ToyPoint.position).Forget();
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}