using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Lines;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
{
    public class FinishLineDisposer : IDisposable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IFinishLineProvider _finishLineProvider;

        public FinishLineDisposer(IFinishLineProvider finishLineProvider, ISceneLoadService sceneLoadService)
        {
            _finishLineProvider = finishLineProvider;
            _sceneLoadService = sceneLoadService;

            _sceneLoadService.OnSceneLoading += OnSceneLoading;
            _sceneLoadService.OnSceneReload += OnSceneReload;
        }

        public void Dispose()
        {
            _sceneLoadService.OnSceneLoading -= OnSceneLoading;
            _sceneLoadService.OnSceneReload -= OnSceneReload;
        }

        private void OnSceneLoading(Scene obj)
        {
            Unload();
        }

        private void OnSceneReload(Scene obj)
        {
            Unload();
        }
        
        private void Unload()
        {
            _finishLineProvider.Line.Dispose();
        }
    }
}