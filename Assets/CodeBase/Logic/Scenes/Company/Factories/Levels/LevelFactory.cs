using System;
using CodeBase.Data.ScriptableObjects.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly ILevelsConfigProvider _levelsConfigProvider;

        public event Action<LevelMediator> OnSpawn;
        
        public LevelFactory(ILevelsConfigProvider levelsConfigProvider)
        {
            _levelsConfigProvider = levelsConfigProvider;
        }

        public async UniTask<LevelMediator> SpawnAsync()
        {
            var levelPrefab = await _levelsConfigProvider.GetLevelPrefabAsync();
            var level = Object.Instantiate(levelPrefab).GetComponent<LevelMediator>();

            OnSpawn?.Invoke(level);
            
            return level;
        }
    }
}