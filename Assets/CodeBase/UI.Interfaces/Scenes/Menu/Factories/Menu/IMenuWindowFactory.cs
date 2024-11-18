using CodeBase.UI.Scenes.Menu.Mediators;
using CodeBase.UI.Scenes.Menu.Mediators.Menu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Menu.Factories.Menu
{
    public interface IMenuWindowFactory
    {
        UniTask<MenuWindowMediator> SpawnAsync();
    }
}