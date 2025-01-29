using System;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.FinishLine;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
{
    public class CompanyFinishLineSpawner : ICompanyFinishLineSpawner, IDisposable
    {
        private readonly IFinishLineFactory _finishLineFactory;
        private readonly IDisposable _disposable;
        private readonly IFinishLineProvider _finishLineProvider;
        private readonly ILevelSizeSystem _levelSizeSystem;

        public event Action<FinishLineMediator> OnSpawn;

        public CompanyFinishLineSpawner(
            ILevelProvider levelProvider,
            IFinishLineFactory finishLineFactory,
            ILevelSizeSystem levelSizeSystem,
            IFinishLineProvider finishLineProvider)
        {
            _levelSizeSystem = levelSizeSystem;
            _finishLineProvider = finishLineProvider;
            _finishLineFactory = finishLineFactory;

            _disposable = levelProvider.Level.Subscribe(OnLevelProviderChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private async void OnLevelProviderChanged(LevelMediator levelMediator)
        {
            if (levelMediator == null)
            {
                return;
            }

            var finishLine = await SpawnFinishLine(levelMediator);
            _finishLineProvider.Register(finishLine);
        }
        
        private async UniTask<FinishLineMediator> SpawnFinishLine(LevelMediator level)
        {
            var height = await _levelSizeSystem.GetHeightAsync();
            var position = level.OriginPoint.position + Vector3.up * height;
            var finishLine = await _finishLineFactory.SpawnAsync(position, level.OriginPoint.rotation);
            
            finishLine.Height.text = $"{height} m";
            
            OnSpawn?.Invoke(finishLine);

            return finishLine;
        }
    }
}