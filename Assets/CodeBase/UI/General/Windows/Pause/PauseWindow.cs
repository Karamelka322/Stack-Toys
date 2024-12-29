using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.UI.General.Mediators.Windows.Pause;
using CodeBase.UI.Interfaces.General.Factories.Windows.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Windows.Pause
{
    public class PauseWindow : BaseWindow
    {
        private readonly IPauseWindowFactory _pauseWindowFactory;
        private readonly ICompanySceneUnload _companySceneUnload;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IWindowService _windowService;

        private PauseWindowMediator _mediator;

        public PauseWindow(IWindowService windowService, IPauseWindowFactory pauseWindowFactory,
            ICompanySceneUnload companySceneUnload, ISceneLoadService sceneLoadService) : base(windowService)
        {
            _windowService = windowService;
            _sceneLoadService = sceneLoadService;
            _companySceneUnload = companySceneUnload;
            _pauseWindowFactory = pauseWindowFactory;
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _pauseWindowFactory.SpawnAsync();
            
            _mediator.MenuButton.onClick.AddListener(OnMenuButtonClicked);
            _mediator.CloseButton.onClick.AddListener(OnCloseButtonClicked);
        }

        public override void Close()
        {
            _mediator.MenuButton.onClick.RemoveAllListeners();
            _mediator.CloseButton.onClick.RemoveAllListeners();
            
            Object.Destroy(_mediator.gameObject);
        }

        private void OnCloseButtonClicked()
        {
            _windowService.CloseAsync<PauseWindow>().Forget();
        }

        private void OnMenuButtonClicked()
        {
            _mediator.MenuButton.onClick.RemoveAllListeners();
            _mediator.CloseButton.onClick.RemoveAllListeners();

            _companySceneUnload.Unload();
            _sceneLoadService.LoadScene(SceneNames.Menu);
        }
    }
}