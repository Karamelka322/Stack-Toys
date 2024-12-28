using System;
using System.Threading;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Observers.Ready
{
    public class InfinitySceneReadyObserver : ISceneReadyObserver, IDisposable
    {
        private readonly IInfinityLevelSpawner _infinityLevelSpawner;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        public BoolReactiveProperty IsReady { get; }

        public InfinitySceneReadyObserver(IInfinityLevelSpawner infinityLevelSpawner)
        {
            _infinityLevelSpawner = infinityLevelSpawner;

            _cancellationTokenSource = new CancellationTokenSource();
            IsReady = new BoolReactiveProperty();
            
            WaitReadyAsync().Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            IsReady?.Dispose();
        }
        
        private async UniTask WaitReadyAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _infinityLevelSpawner.IsSpawned.Value == false,
                    cancellationToken: _cancellationTokenSource.Token);
            
                IsReady.Value = true;
            }
            catch (OperationCanceledException e) { }
        }
    }
}