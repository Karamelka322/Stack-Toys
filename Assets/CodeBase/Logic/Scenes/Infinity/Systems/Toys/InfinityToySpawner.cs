using System;
using CodeBase.Logic.General.Extensions;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
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
        private readonly IDisposable _disposable;

        public InfinityToySpawner(IToyChoicerFactory toyChoicerFactory, ILevelBorderSystem levelBorderSystem,
            IInfinitySceneToySettingsProvider toySettingsProvider, IToyChoicerProvider toyChoicerProvider,
            IInfinityLevelSpawner levelSpawner)
        {
            _toyChoicerProvider = toyChoicerProvider;
            _toySettingsProvider = toySettingsProvider;
            _toyChoicerFactory = toyChoicerFactory;
            _levelBorderSystem = levelBorderSystem;

            _disposable = levelSpawner.IsSpawned.Subscribe(OnLevelSpawn);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnLevelSpawn(bool isSpawned)
        {
            if (isSpawned == false)
            {
                return;
            }
            
            SpawnAsync().Forget();
        }

        private async UniTask SpawnAsync()
        {
            var toyAssets = await _toySettingsProvider.GetToysAsync();
            var position = _levelBorderSystem.OriginPoint + Vector3.up * 2f;

            var randomToyAsset1 = toyAssets.Random();
            var randomToyAsset2 = toyAssets.Random();
            
            var toyChoicer = await _toyChoicerFactory.
                SpawnAsync(randomToyAsset1, randomToyAsset2, position);
            
            _toyChoicerProvider.Register(toyChoicer.Item1, toyChoicer.Item2);
        }
    }
}