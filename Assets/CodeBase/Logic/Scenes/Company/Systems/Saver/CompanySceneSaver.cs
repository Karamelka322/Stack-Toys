using System;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Company.Systems.Saver
{
    public class CompanySceneSaver : IDisposable
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;
        private readonly ISceneLoadService _sceneLoadService;

        public CompanySceneSaver(IPlayerSaveDataProvider playerSaveDataProvider, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _playerSaveDataProvider = playerSaveDataProvider;

            _sceneLoadService.OnSceneLoading += OnSceneLoading;
            _sceneLoadService.OnSceneReload += OnSceneLoading;
            
            Application.focusChanged += OnFocusChanged;
            Application.quitting += OnClosing;
        }

        public void Dispose()
        {
            _sceneLoadService.OnSceneLoading -= OnSceneLoading;
            _sceneLoadService.OnSceneReload -= OnSceneLoading;

            Application.quitting -= OnClosing;
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnSceneLoading(Scene scene)
        {
            _playerSaveDataProvider.Save();
        }

        private void OnFocusChanged(bool isFocused)
        {
            if (isFocused == false)
            {
                _playerSaveDataProvider.Save();
            }
        }

        private void OnClosing()
        {
            _playerSaveDataProvider.Save();
        }
    }
}