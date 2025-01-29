using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Localizations;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Levels;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Levels;
using CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class LevelsWindow : BaseWindow, ILevelsWindow, IDisposable
    {
        private readonly ILevelsWindowFactory _levelsWindowFactory;
        private readonly IWindowService _windowService;
        private readonly ILocalizationService _localizationService;

        private LevelsWindowMediator _mediator;
        
        public BoolReactiveProperty IsOpen { get; }

        public LevelsWindow(
            ILevelsWindowFactory levelsWindowFactory,
            IWindowService windowService,
            ILocalizationService localizationService) : base(windowService)
        {
            _localizationService = localizationService;
            _windowService = windowService;
            _levelsWindowFactory = levelsWindowFactory;

            IsOpen = new BoolReactiveProperty();
            
            _localizationService.OnLocaleChanged += OnLocaleChanged;
        }
    
        public void Dispose()
        {
            _localizationService.OnLocaleChanged -= OnLocaleChanged;

            IsOpen?.Dispose();
        }
        
        public override async UniTask OpenAsync()
        {
            _mediator = await _levelsWindowFactory.SpawnAsync();
            
            LocalizeTitleAsync().Forget();
            
            _mediator.BackButton.onClick.AddListener(() => _windowService.CloseAsync<LevelsWindow>());
            
            IsOpen.Value = true;
        }

        public override void Close()
        {
            _mediator.BackButton.onClick.RemoveAllListeners();
            Object.Destroy(_mediator.gameObject);

            IsOpen.Value = false;
        }
        
        private void OnLocaleChanged()
        {
            LocalizeTitleAsync().Forget();
        }

        private async UniTask LocalizeTitleAsync()
        {
            _mediator.Title.text = await _localizationService.LocalizeAsync(LocalizationConstants.Levels);
        }
    }
}