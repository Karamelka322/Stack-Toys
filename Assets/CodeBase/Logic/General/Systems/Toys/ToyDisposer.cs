using System;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToyDisposer : IDisposable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IToyProvider _toyProvider;

        public ToyDisposer(ISceneLoadService sceneLoadService, IToyProvider toyProvider)
        {
            _toyProvider = toyProvider;
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
            Unload();
        }

        private void OnSceneReload(Scene scene)
        {
            Unload();
        }

        private void Unload()
        {
            foreach (var toy in _toyProvider.Toys)
            {
                toy.Item2.Reset();
            }
            
            _toyProvider.Dispose();
        }
    }
}