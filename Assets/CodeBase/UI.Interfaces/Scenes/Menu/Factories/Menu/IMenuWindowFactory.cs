using CodeBase.UI.Scenes.Menu.Mediators.Windows.Menu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Menu.Factories.Menu
{
    public interface IMenuWindowFactory
    {
        UniTask<MenuWindowMediator> SpawnAsync();
    }
}