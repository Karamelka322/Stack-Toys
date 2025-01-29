using System;
using System.Threading;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Levels
{
    public class InfinityLevelSizeSystem : BaseLevelSizeSystem, IDisposable
    {
        private readonly IInfinityRecordSystem _recordSystem;
        private readonly ILevelProvider _levelProvider;

        private readonly AsyncLazy _readyTask;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private float _width;
        
        public InfinityLevelSizeSystem(
            ILevelProvider levelProvider,
            IInfinityRecordSystem recordSystem,
            ICameraFormulas cameraFormulas) : base(cameraFormulas)
        {
            _levelProvider = levelProvider;
            _recordSystem = recordSystem;

            _readyTask = UniTask.Lazy(WaitReadyAsync);

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override async UniTask<float> GetHeightAsync()
        {
            await _readyTask;
            
            return _recordSystem.WorldRecord.Value;
        }

        public override async UniTask<float> GetWidthAsync()
        {
            await _readyTask;

            if (_width <= 0)
            {
                _width = CalculateWidth(_levelProvider.Level.Value.OriginPoint);
            }
            
            return _width;
        }

        private async UniTask WaitReadyAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _recordSystem.IsReady.Value == false,
                    cancellationToken: _cancellationTokenSource.Token);
            
                await UniTask.WaitWhile(() => _levelProvider.Level.Value == null,
                    cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e) { }
        }
    }
}