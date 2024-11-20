using System;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Company.Factories
{
    public interface ILevelFactory
    {
        event Action<LevelMediator> OnSpawn;
        UniTask<LevelMediator> SpawnAsync();
    }
}