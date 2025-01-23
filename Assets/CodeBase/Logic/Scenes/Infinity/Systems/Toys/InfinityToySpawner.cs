using System;
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

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class InfinityToySpawner : IDisposable
    {
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IToyChoicerFactory _toyChoicerFactory;
        private readonly IInfinitySceneToySettingsProvider _toySettingsProvider;
        private readonly IToyChoicerProvider _toyChoicerProvider;
        private readonly IToyDestroyer _toyDestroyer;

        private readonly CompositeDisposable _compositeDisposable;

        public InfinityToySpawner(IToyChoicerFactory toyChoicerFactory, ILevelBorderSystem levelBorderSystem,
            IInfinitySceneToySettingsProvider toySettingsProvider, IToyChoicerProvider toyChoicerProvider,
            IInfinityLevelSpawner levelSpawner, IToyTowerObserver toyTowerObserver, IToyDestroyer toyDestroyer)
        {
            _toyDestroyer = toyDestroyer;
            _toyChoicerProvider = toyChoicerProvider;
            _toySettingsProvider = toySettingsProvider;
            _toyChoicerFactory = toyChoicerFactory;
            _levelBorderSystem = levelBorderSystem;

            _compositeDisposable = new CompositeDisposable();

            _toyDestroyer.OnDestroyAll += OnDestroyAll;
            
            toyTowerObserver.Tower.ObserveAdd().Subscribe(OnIncreasedTower).AddTo(_compositeDisposable);
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
            
            var randomToyAsset1 = toyAssets.Random();
            var randomToyAsset2 = toyAssets.Random();
            
            var toyChoicer = await _toyChoicerFactory.
                SpawnAsync(randomToyAsset1, randomToyAsset2, GetSpawnPoint());
            
            _toyChoicerProvider.Register(toyChoicer);
        }

        private Vector3 GetSpawnPoint()
        {
            return _levelBorderSystem.OriginPoint + Vector3.up * 2f;
        }
    }
}