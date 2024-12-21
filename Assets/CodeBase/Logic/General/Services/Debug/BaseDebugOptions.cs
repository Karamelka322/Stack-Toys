using System;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using UniRx;

namespace CodeBase.CodeBase.Logic.Services.Debug
{
    public abstract class BaseDebugOptions : IDisposable
    {
        private readonly IDebugService _debugService;
        private readonly IDisposable _disposable;

        protected BaseDebugOptions(IDebugService debugService, ISceneReadyObserver sceneReady)
        {
            _debugService = debugService;
            _disposable = sceneReady.IsReady.Subscribe(OnSceneReady);
        }

        private void OnSceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }
            
            _debugService.RegisterOptionContainer(this);
        }
        
        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
            _debugService.UnregisterOptionContainer(this);
        }
    }
}