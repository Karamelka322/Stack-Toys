using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Menu;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Levels;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Menu;
using CodeBase.UI.Scenes.Menu.Mediators.Menu;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Menu
{
    public class MenuWindow : IMenuWindow
    {
        private readonly IMenuWindowFactory _menuWindowFactory;
        private readonly ILevelsWindow _levelsWindow;

        private MenuWindowMediator _mediator;

        public MenuWindow(IMenuWindowFactory menuWindowFactory, ILevelsWindow levelsWindow)
        {
            _levelsWindow = levelsWindow;
            _menuWindowFactory = menuWindowFactory;
        }

        public async UniTask OpenAsync()
        {
            _mediator = await _menuWindowFactory.SpawnAsync();
            
            _mediator.CompanyButton.onClick.AddListener(OnCompanyButtonClicked);
        }

        public void Close()
        {
            Object.Destroy(_mediator.gameObject);
        }

        private async void OnCompanyButtonClicked()
        {
            _mediator.CompanyButton.onClick.RemoveAllListeners();
            
            await _levelsWindow.OpenAsync();
            Close();
        }
    }
}