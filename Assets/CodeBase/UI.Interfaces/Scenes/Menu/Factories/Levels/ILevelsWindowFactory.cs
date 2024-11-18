using CodeBase.UI.Scenes.Menu.Mediators.Levels;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Menu.Factories.Levels
{
    public interface ILevelsWindowFactory
    {
        UniTask<LevelsWindowMediator> SpawnAsync();
    }
}