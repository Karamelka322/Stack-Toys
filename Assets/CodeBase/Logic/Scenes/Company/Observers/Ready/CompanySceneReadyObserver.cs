using System;
using System.Threading;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Lines;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Observers.Ready
{
    public class CompanySceneReadyObserver : ISceneReadyObserver, IDisposable
    {
        private readonly ILevelProvider _levelProvider;
        private readonly IToyProvider _toyProvider;
        private readonly IFinishLineProvider _finishLineProvider;
        private readonly ICompanyMainWindow _companyMainWindow;

        private readonly CancellationTokenSource _cancellationTokenSource;

        public BoolReactiveProperty IsReady { get; }
        
        public CompanySceneReadyObserver(
            ILevelProvider levelProvider,
            IToyProvider toyProvider,
            IFinishLineProvider finishLineProvider,
            ICompanyMainWindow companyMainWindow)
        {
            _companyMainWindow = companyMainWindow;
            _finishLineProvider = finishLineProvider;
            _toyProvider = toyProvider;
            _levelProvider = levelProvider;

            _cancellationTokenSource = new CancellationTokenSource();
            IsReady = new BoolReactiveProperty();

            WaitReadyAsync().Forget();
        }

        private async UniTask WaitReadyAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _levelProvider.Level.Value == null,
                    cancellationToken: _cancellationTokenSource.Token);
                
                await UniTask.WaitWhile(() => _finishLineProvider.Line.Value == null,
                    cancellationToken: _cancellationTokenSource.Token);
                
                await UniTask.WaitWhile(() => _toyProvider.Toys.Count == 0,
                    cancellationToken: _cancellationTokenSource.Token);
                
                await UniTask.WaitWhile(() => _companyMainWindow.IsOpened.Value == false,
                    cancellationToken: _cancellationTokenSource.Token);
                
                IsReady.Value = true;
            }
            catch (OperationCanceledException exception) { }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            IsReady?.Dispose();
        }
    }
}