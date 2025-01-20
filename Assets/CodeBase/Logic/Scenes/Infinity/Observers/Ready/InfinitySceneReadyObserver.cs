using System;
using System.Threading;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Observers.Ready
{
    public class InfinitySceneReadyObserver : ISceneReadyObserver, IDisposable
    {
        private readonly IInfinityLevelSpawner _infinityLevelSpawner;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IToyChoicerProvider _toyChoicerProvider;

        public BoolReactiveProperty IsReady { get; }

        public InfinitySceneReadyObserver(IInfinityLevelSpawner infinityLevelSpawner, IToyChoicerProvider toyChoicerProvider)
        {
            _toyChoicerProvider = toyChoicerProvider;
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
                
                await UniTask.WaitWhile(() => _toyChoicerProvider.ToyChoicers.Count == 0,
                    cancellationToken: _cancellationTokenSource.Token);
                
                IsReady.Value = true;
            }
            catch (OperationCanceledException e) { }
        }
    }
}