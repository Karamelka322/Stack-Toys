using System;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.General.Factories.Finish;
using CodeBase.Logic.Interfaces.General.Systems.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Finish
{
    public class FinishLineSpawner : IFinishLineSpawner, IDisposable
    {
        private readonly IFinishLineFactory _finishLineFactory;
        private readonly IDisposable _disposable;

        public event Action<FinishLineMediator> OnSpawn;

        public FinishLineSpawner(ILevelProvider levelProvider, IFinishLineFactory finishLineFactory)
        {
            _finishLineFactory = finishLineFactory;

            _disposable = levelProvider.Level.Subscribe(OnLevelProviderChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnLevelProviderChanged(LevelMediator levelMediator)
        {
            if (levelMediator == null)
            {
                return;
            }

            SpawnFinishLine(levelMediator);
        }

        private async void SpawnFinishLine(LevelMediator level)
        {
            var position = level.OriginPoint.position + Vector3.up * level.Height;
            var finishLine = await _finishLineFactory.SpawnAsync(position, level.OriginPoint.rotation);
            
            finishLine.Height.text = $"{level.Height} m";
            
            OnSpawn?.Invoke(finishLine);
        }
    }
}