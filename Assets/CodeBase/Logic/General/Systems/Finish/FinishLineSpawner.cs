using System;
using CodeBase.Logic.Interfaces.General.Factories.Finish;
using CodeBase.Logic.Interfaces.General.Systems.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Finish
{
    public class FinishLineSpawner : IFinishLineSpawner, IDisposable
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IFinishLineFactory _finishLineFactory;

        public FinishLineSpawner(ILevelFactory levelFactory, IFinishLineFactory finishLineFactory)
        {
            _finishLineFactory = finishLineFactory;
            _levelFactory = levelFactory;
            
            levelFactory.OnSpawn += OnSpawnLevel;
        }

        public void Dispose()
        {
            _levelFactory.OnSpawn -= OnSpawnLevel;
        }

        private async void OnSpawnLevel(LevelMediator level)
        {
            var position = level.OriginPoint.position + Vector3.up * level.Height;
            var finishLine = await _finishLineFactory.SpawnAsync(position, level.OriginPoint.rotation);
            
            finishLine.Height.text = $"{level.Height} m";
        }
    }
}