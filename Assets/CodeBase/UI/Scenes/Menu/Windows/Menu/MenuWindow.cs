using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Menu;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Menu;
using CodeBase.UI.Scenes.Menu.Mediators.Windows.Menu;
using CodeBase.UI.Scenes.Menu.Windows.Levels;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Menu
{
    public class MenuWindow : BaseWindow, IMenuWindow
    {
        private readonly IMenuWindowFactory _menuWindowFactory;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IWindowService _windowService;

        private MenuWindowMediator _mediator;

        public BoolReactiveProperty IsShowing { get; }

        public MenuWindow(
            IMenuWindowFactory menuWindowFactory,
            IWindowService windowService) : base(windowService)
        {
            _windowService = windowService;
            _menuWindowFactory = menuWindowFactory;

            IsShowing = new BoolReactiveProperty();
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _menuWindowFactory.SpawnAsync();
            _mediator.CompanyButton.onClick.AddListener(OnCompanyButtonClicked);
            
            IsShowing.Value = true;
        }

        public override void Show()
        {
            _mediator.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            _mediator.gameObject.SetActive(false);
        }

        public override void Close()
        {
            _mediator.CompanyButton.onClick.RemoveAllListeners();
            Object.Destroy(_mediator.gameObject);
        }

        private async void OnCompanyButtonClicked()
        {
            await _windowService.OpenAsync<LevelsWindow>();
        }
    }
}