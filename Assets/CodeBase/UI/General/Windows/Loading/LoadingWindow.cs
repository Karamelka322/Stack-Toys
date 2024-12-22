using System;
using System.Threading;
using CodeBase.UI.General.Mediators.Windows.Loading;
using CodeBase.UI.Interfaces.General.Factories.Windows.Loading;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.General.Windows.Loading
{
    public class LoadingWindow : ILoadingWindow
    {
        private readonly ILoadingWindowFactory _loadingWindowFactory;
        
        private readonly int Show = Animator.StringToHash("Show");
        private readonly int Loop = Animator.StringToHash("Loop");
        private readonly int Hide = Animator.StringToHash("Hide");
        
        private CancellationTokenSource _cancellationTokenSource;
        
        private LoadingWindowMediator _mediator;

        public LoadingWindow(ILoadingWindowFactory loadingWindowFactory)
        {
            _loadingWindowFactory = loadingWindowFactory;
        }
        
        public void Open()
        {
            if (_mediator != null)
            {
                return;
            }
            
            _cancellationTokenSource = new CancellationTokenSource();
            _mediator = _loadingWindowFactory.Spawn();
        }

        public async UniTask ShowAsync()
        {
            _mediator.Animator.Play(Show);
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e) { }
        }

        public async UniTask HideAsync()
        {
            _mediator.Animator.Play(Hide);

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e) { }
        }
        
        public void Close()
        {
            if (_mediator == null)
            {
                return;
            }
            
            _cancellationTokenSource?.Cancel();
            Object.Destroy(_mediator.gameObject);
        }
    }
}