using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.Scenes.Menu.Windows.Menu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Menu.Presenters.Menu
{
    public class MenuWindowPresenter
    {
        public MenuWindowPresenter(IWindowService windowService)
        {
            windowService.OpenAsync<MenuWindow>().Forget();
        }
    }
}