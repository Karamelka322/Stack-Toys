using System;
using System.Linq;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToySpawner : IDisposable
    {
        private const float DistanceToFinish = 3f;
        private const float OffsetFromFinishLine = 1.1f;
        private const float OffsetFromTower = 2f;
        
        private readonly IToyTowerObserver _toyTowerObserver;
        private readonly IToyFactory _toyFactory;
        private readonly ILevelBorderSystem _levelBorderSystem;

        private readonly CompositeDisposable _compositeDisposable;
        
        public ToySpawner(
            IToyTowerObserver toyTowerObserver,
            IToyFactory toyFactory,
            ICompanySceneLoad companySceneLoad,
            ILevelBorderSystem levelBorderSystem)
        {
            _levelBorderSystem = levelBorderSystem;
            _toyFactory = toyFactory;
            _toyTowerObserver = toyTowerObserver;

            _compositeDisposable = new CompositeDisposable();

            companySceneLoad.IsLoaded.Subscribe(OnSceneLoad).AddTo(_compositeDisposable);
            _toyTowerObserver.Tower.ObserveAdd().Subscribe(OnIncreaseToyTower).AddTo(_compositeDisposable);
        }

        private void OnSceneLoad(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }

            Spawn();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnIncreaseToyTower(CollectionAddEvent<ToyMediator> addEvent)
        {
            Spawn();
        }

        private void Spawn()
        {
            var point = GetSpawnPoint();
            _toyFactory.SpawnAsync(point).Forget();
        }

        private Vector3 GetSpawnPoint()
        {
            var maxHeight = _levelBorderSystem.TopLeftPoint.y;
            
            var startPosition = _toyTowerObserver.Tower.Count == 0 ? 
                _levelBorderSystem.OriginPoint :
                _toyTowerObserver.Tower.Last().transform.position;

            if (maxHeight > startPosition.y + DistanceToFinish)
            {
                return startPosition + Vector3.up * OffsetFromTower;
            }
            else
            {
                return _levelBorderSystem.OriginPoint + Vector3.up * maxHeight + Vector3.up * OffsetFromFinishLine;
            }
        }
    }
}