using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Menu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Menu.Presenters.Menu
{
    public class MenuWindowPresenter
    {
        public MenuWindowPresenter(IMenuWindow menuWindow)
        {
            menuWindow.OpenAsync().Forget();
        }
    }
}