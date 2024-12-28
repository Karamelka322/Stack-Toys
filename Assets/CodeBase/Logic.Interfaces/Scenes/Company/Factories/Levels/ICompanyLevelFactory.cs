using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels
{
    public interface ICompanyLevelFactory
    {
        UniTask<LevelMediator> SpawnAsync(int levelIndex);
    }
}