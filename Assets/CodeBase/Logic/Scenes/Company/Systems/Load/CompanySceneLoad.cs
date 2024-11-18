using CodeBase.Data.ScriptableObjects.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public class CompanySceneLoad : IInitializable
    {
        private readonly ILevelsConfigProvider _levelsConfigProvider;

        public CompanySceneLoad(ILevelsConfigProvider levelsConfigProvider)
        {
            _levelsConfigProvider = levelsConfigProvider;
        }
        
        public async void Initialize()
        {
            var levelPrefab = await _levelsConfigProvider.GetLevelPrefabAsync();
            var level = Object.Instantiate(levelPrefab).GetComponent<Level>();

            Camera.main.UpdateVolumeStack();
            Camera.main.transform.position = level.CameraPoint.position;
        }
    }
}