using System;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Factories.Levels
{
    public class LevelFactory : ILevelFactory
    {
        private readonly ILevelsConfigProvider _levelsConfigProvider;

        public event Action<LevelMediator> OnSpawn;
        
        public LevelFactory(ILevelsConfigProvider levelsConfigProvider)
        {
            _levelsConfigProvider = levelsConfigProvider;
        }

        public async UniTask<LevelMediator> SpawnAsync(int levelIndex)
        {
            var levelPrefab = await _levelsConfigProvider.GetLevelPrefabAsync(levelIndex);
            var level = Object.Instantiate(levelPrefab).GetComponent<LevelMediator>();

            OnSpawn?.Invoke(level);
            
            return level;
        }
    }
}