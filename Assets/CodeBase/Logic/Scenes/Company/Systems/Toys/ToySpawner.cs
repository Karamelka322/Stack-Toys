using System;
using System.Linq;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToySpawner : IToySpawner, IDisposable
    {
        private const float DistanceToFinish = 3f;
        private const float OffsetFromFinishLine = 1.1f;
        private const float OffsetFromTower = 2f;
        
        private readonly IToyTowerObserver _toyTowerObserver;
        private readonly IToyFactory _toyFactory;
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IToyProvider _toyProvider;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ICompanyLevelsSettingProvider _levelsConfigProvider;
        private readonly IToyCountObserver _toyCountObserver;
        private readonly IToyDestroyer _toyDestroyer;

        private readonly CompositeDisposable _compositeDisposable;

        public event Action<ToyMediator, ToyStateMachine> OnSpawn;
        
        public ToySpawner(
            IToyTowerObserver toyTowerObserver,
            IToyFactory toyFactory,
            IToyProvider toyProvider,
            IToyDestroyer toyDestroyer,
            ILevelSpawner companySceneLoad,
            IToyCountObserver toyCountObserver,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            ICompanyLevelsSettingProvider levelsConfigProvider,
            ILevelBorderSystem levelBorderSystem)
        {
            _toyDestroyer = toyDestroyer;
            _toyCountObserver = toyCountObserver;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelsConfigProvider = levelsConfigProvider;
            _toyProvider = toyProvider;
            _levelBorderSystem = levelBorderSystem;
            _toyFactory = toyFactory;
            _toyTowerObserver = toyTowerObserver;

            _compositeDisposable = new CompositeDisposable();

            toyTowerObserver.Tower.ObserveAdd().Subscribe(OnIncreaseToyTower).AddTo(_compositeDisposable);
            companySceneLoad.IsLoaded.Subscribe(OnSceneLoad).AddTo(_compositeDisposable);

            _toyDestroyer.OnDestroyAll += OnTowerDestroy;
        }

        private void OnSceneLoad(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }

            SpawnAsync().Forget();
        }

        public void Dispose()
        {
            _toyDestroyer.OnDestroyAll -= OnTowerDestroy;
            _compositeDisposable?.Dispose();
        }

        private void OnIncreaseToyTower(CollectionAddEvent<ToyMediator> toy)
        {
            if (_toyCountObserver.LeftAvailableNumberOfToys.Value > 0)
            {
                SpawnAsync().Forget();
            }
        }
        
        private void OnTowerDestroy()
        {
            SpawnAsync().Forget();
        }

        private async UniTask SpawnAsync()
        {
            var point = GetSpawnPoint();
            var prefab = await GetToyPrefabAsync();
            var toy = _toyFactory.Spawn(prefab, point);

            _toyProvider.Register(toy.Item1, toy.Item2);
            OnSpawn?.Invoke(toy.Item1, toy.Item2);
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

        private async UniTask<GameObject> GetToyPrefabAsync()
        {
            var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var prefabs = await _levelsConfigProvider.GetToyPrefabsAsync(currentLevel);

            return prefabs[_toyProvider.Toys.Count];
        }
    }
}