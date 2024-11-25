using System;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels
{
    public interface ILevelFactory
    {
        event Action<LevelMediator> OnSpawn;
        UniTask<LevelMediator> SpawnAsync(int levelIndex);
    }
}