using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CodeBase.Logic.General.Extensions;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Data;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels;
using CodeBase.Logic.Scenes.Infinity.Providers.Lines;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class InfinityToySpawner : IDisposable
    {
        private const float LineMinDistance = 0.8f;
        private const float LineOffset = 2f;
        
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IToyChoicerFactory _toyChoicerFactory;
        private readonly IInfinitySceneToySettingsProvider _toySettingsProvider;
        private readonly IToyChoicerProvider _toyChoicerProvider;
        private readonly IToyTowerBuildObserver _toyTowerBuildObserver;
        private readonly IToyDestroyer _toyDestroyer;
        private readonly IInfinityLevelSpawner _levelSpawner;
        private readonly IRecordLineProvider _recordLineProvider;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly List<AssetReferenceGameObject> _cache;

        public InfinityToySpawner(
            IToyChoicerFactory toyChoicerFactory,
            ILevelBorderSystem levelBorderSystem,
            IInfinitySceneToySettingsProvider toySettingsProvider,
            IToyChoicerProvider toyChoicerProvider,
            IInfinityLevelSpawner levelSpawner,
            IRecordLineProvider recordLineProvider,
            IToyTowerBuildObserver toyTowerBuildObserver, 
            IToyDestroyer toyDestroyer)
        {
            _recordLineProvider = recordLineProvider;
            _levelSpawner = levelSpawner;
            _toyTowerBuildObserver = toyTowerBuildObserver;
            _toyDestroyer = toyDestroyer;
            _toyChoicerProvider = toyChoicerProvider;
            _toySettingsProvider = toySettingsProvider;
            _toyChoicerFactory = toyChoicerFactory;
            _levelBorderSystem = levelBorderSystem;

            _cancellationTokenSource = new CancellationTokenSource();
            _compositeDisposable = new CompositeDisposable();
            _cache = new List<AssetReferenceGameObject>();

            _toyDestroyer.OnDestroyAll += OnDestroyAll;

            toyTowerBuildObserver.Tower.ObserveAdd().Subscribe(OnIncreasedTower).AddTo(_compositeDisposable);

            InitializeAsync().Forget();
        }
        
        public void Dispose()
        {
            _toyDestroyer.OnDestroyAll -= OnDestroyAll;
            
            _cancellationTokenSource?.Cancel();
            _compositeDisposable?.Dispose();
        }
        
        private async UniTask InitializeAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _levelSpawner.IsSpawned.Value == false, 
                    cancellationToken: _cancellationTokenSource.Token);
                
                // await UniTask.WaitWhile(() => _recordLineProvider.PlayerRecordLine.Value == null, 
                //     cancellationToken: _cancellationTokenSource.Token);
                //
                // await UniTask.WaitWhile(() => _recordLineProvider.WorldRecordLine.Value == null, 
                //     cancellationToken: _cancellationTokenSource.Token);
                
                SpawnAsync().Forget();
            }
            catch (OperationCanceledException e) { }
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
            var towerOffset = Vector3.zero;
            
            if (_toyTowerBuildObserver.Tower.Count > 0)
            {
                towerOffset += Vector3.up * _toyTowerBuildObserver.Tower.Last().transform.position.y + Vector3.up;
            }
            else
            {
                towerOffset += Vector3.up * LineOffset;
            }

            var position = _levelBorderSystem.OriginPoint + towerOffset;
            
            if (_recordLineProvider.PlayerRecordLine.Value != null)
            {
                var playerRecordLine = _recordLineProvider.PlayerRecordLine.Value.GetPosition();
            
                if (Vector3.Distance(position, playerRecordLine) < LineMinDistance)
                {
                    position = playerRecordLine + Vector3.up * LineOffset;
                }   
            }

            if (_recordLineProvider.WorldRecordLine.Value != null)
            {
                var worldRecordLine = _recordLineProvider.WorldRecordLine.Value.GetPosition();
            
                if (Vector3.Distance(position, worldRecordLine) < LineMinDistance)
                {
                    position = worldRecordLine + Vector3.up * LineOffset;
                }   
            }
            
            return position;
        }
    }
}