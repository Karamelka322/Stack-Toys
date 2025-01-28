using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class CameraDisposer : IDisposable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICameraStateMachine _cameraStateMachine;

        public CameraDisposer(ISceneLoadService sceneLoadService, ICameraStateMachine cameraStateMachine)
        {
            _cameraStateMachine = cameraStateMachine;
            _sceneLoadService = sceneLoadService;

            _sceneLoadService.OnSceneLoading += OnSceneLoading;
            _sceneLoadService.OnSceneReload += OnSceneReload;
        }

        public void Dispose()
        {
            _sceneLoadService.OnSceneLoading -= OnSceneLoading;
            _sceneLoadService.OnSceneReload -= OnSceneReload;
        }

        private void OnSceneLoading(Scene scene)
        {
            _cameraStateMachine.Dispose();
        }

        private void OnSceneReload(Scene scene)
        {
            _cameraStateMachine.Dispose();
        }
    }
}