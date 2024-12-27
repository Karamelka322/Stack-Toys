using System;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Scenes.Company.Systems
{
    public class CompanySceneSaver : IDisposable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public CompanySceneSaver(ISceneLoadService sceneLoadService, IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
            _sceneLoadService = sceneLoadService;
            
            sceneLoadService.OnSceneReload += OnSceneReload;
        }
        
        public void Dispose()
        {
            _sceneLoadService.OnSceneReload += OnSceneReload;
        }
        
        private void OnSceneReload(Scene scene)
        {
            _playerSaveDataProvider.Save();
        }
    }
}