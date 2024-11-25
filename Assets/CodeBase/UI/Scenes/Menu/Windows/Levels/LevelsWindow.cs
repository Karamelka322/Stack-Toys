using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Levels;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Levels;
using CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class LevelsWindow : BaseWindow, ILevelsWindow
    {
        private readonly ILevelsWindowFactory _levelsWindowFactory;
        private readonly IWindowService _windowService;

        private LevelsWindowMediator _mediator;

        public LevelsWindow(ILevelsWindowFactory levelsWindowFactory, IWindowService windowService) : base(windowService)
        {
            _windowService = windowService;
            _levelsWindowFactory = levelsWindowFactory;
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _levelsWindowFactory.SpawnAsync();
            _mediator.BackButton.onClick.AddListener(() => _windowService.CloseAsync<LevelsWindow>());
        }

        public override void Close()
        {
            _mediator.BackButton.onClick.RemoveAllListeners();
            Object.Destroy(_mediator.gameObject);
        }
    }
}