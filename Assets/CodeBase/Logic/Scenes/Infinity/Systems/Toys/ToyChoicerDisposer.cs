using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class ToyChoicerDisposer : IDisposable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IToyChoicerProvider _toyChoicerProvider;

        public ToyChoicerDisposer(IToyChoicerProvider toyChoicerProvider, ISceneLoadService sceneLoadService)
        {
            _toyChoicerProvider = toyChoicerProvider;
            _sceneLoadService = sceneLoadService;

            _sceneLoadService.OnSceneLoading += OnSceneLoading;
            _sceneLoadService.OnSceneReload += OnSceneLoading;
        }

        public void Dispose()
        {
            _sceneLoadService.OnSceneLoading -= OnSceneLoading;
            _sceneLoadService.OnSceneReload -= OnSceneLoading;
        }

        private void OnSceneLoading(Scene obj)
        {
            foreach (var choicer in _toyChoicerProvider.ToyChoicers)
            {
                choicer.Dispose();
            }
        }
    }
}