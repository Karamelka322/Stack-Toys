using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Levels
{
    public interface IInfinityLevelFactory
    {
        UniTask<LevelMediator> SpawnAsync();
    }
}