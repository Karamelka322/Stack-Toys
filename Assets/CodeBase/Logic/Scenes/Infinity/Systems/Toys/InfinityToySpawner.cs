using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.General.Extensions;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Data;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class InfinityToySpawner : IDisposable
    {
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IToyChoicerFactory _toyChoicerFactory;
        private readonly IInfinitySceneToySettingsProvider _toySettingsProvider;
        private readonly IToyChoicerProvider _toyChoicerProvider;
        private readonly IToyTowerBuildObserver _toyTowerBuildObserver;
        private readonly IToyDestroyer _toyDestroyer;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly List<AssetReferenceGameObject> _cache;

        public InfinityToySpawner(IToyChoicerFactory toyChoicerFactory, ILevelBorderSystem levelBorderSystem,
            IInfinitySceneToySettingsProvider toySettingsProvider, IToyChoicerProvider toyChoicerProvider,
            IInfinityLevelSpawner levelSpawner, IToyTowerBuildObserver toyTowerBuildObserver,
            IToyDestroyer toyDestroyer)
        {
            _toyTowerBuildObserver = toyTowerBuildObserver;
            _toyDestroyer = toyDestroyer;
            _toyChoicerProvider = toyChoicerProvider;
            _toySettingsProvider = toySettingsProvider;
            _toyChoicerFactory = toyChoicerFactory;
            _levelBorderSystem = levelBorderSystem;

            _compositeDisposable = new CompositeDisposable();
            _cache = new List<AssetReferenceGameObject>();

            _toyDestroyer.OnDestroyAll += OnDestroyAll;

            toyTowerBuildObserver.Tower.ObserveAdd().Subscribe(OnIncreasedTower).AddTo(_compositeDisposable);
            levelSpawner.IsSpawned.Subscribe(OnLevelSpawn).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _toyDestroyer.OnDestroyAll -= OnDestroyAll;

            _compositeDisposable?.Dispose();
        }

        private void OnLevelSpawn(bool isSpawned)
        {
            if (isSpawned == false)
            {
                return;
            }
            
            SpawnAsync().Forget();
        }

        private void OnIncreasedTower(CollectionAddEvent<ToyMediator> addEvent)
        {
            SpawnAsync().Forget();
        }
        
        private void OnDestroyAll()
        {
            SpawnAsync().Forget();
        }

        private async UniTask SpawnAsync()
        {
            var toyAssets = await _toySettingsProvider.GetToysAsync();
            
            var randomToyAsset1 = toyAssets.Random(_cache);
            _cache.Add(randomToyAsset1);

            var randomToyAsset2 = toyAssets.Random(_cache);
            _cache.Add(randomToyAsset2);
            
            var toyChoicer = await _toyChoicerFactory.
                SpawnAsync(randomToyAsset1, randomToyAsset2, GetSpawnPoint());
            
            _toyChoicerProvider.Register(toyChoicer);

            if (_cache.Count >= toyAssets.Length)
            {
                _cache.Clear();
            }
        }

        private Vector3 GetSpawnPoint()
        {
            Vector3 offset = Vector3.zero;
            
            if (_toyTowerBuildObserver.Tower.Count > 0)
            {
                
                offset += Vector3.up * _toyTowerBuildObserver.Tower.Last().transform.position.y + Vector3.up;
            }
            else
            {
                offset += Vector3.up * 2f;
            }
            
            return _levelBorderSystem.OriginPoint + offset;
        }
    }
}