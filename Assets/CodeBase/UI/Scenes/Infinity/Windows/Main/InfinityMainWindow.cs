using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Windows.Main;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.Scenes.Infinity.Windows.Main
{
    public class InfinityMainWindow : BaseWindow, IInfinityMainWindow
    {
        private readonly IInfinityMainWindowFactory _windowFactory;
        private readonly IWindowService _windowService;

        private CompositeDisposable _compositeDisposable;
        private InfinityMainWindowReferences _references;

        public ToyRotatorElement ToyRotatorElement => _references.ToyRotatorElement;
        public BoolReactiveProperty IsOpened { get; }
        
        public InfinityMainWindow(
            IInfinityMainWindowFactory windowFactory,
            IWindowService windowService) : base(windowService)
        {
            _windowService = windowService;
            _windowFactory = windowFactory;

            IsOpened = new BoolReactiveProperty();
        }

        public override async UniTask OpenAsync()
        {
            _references = await _windowFactory.SpawnAsync();

            _compositeDisposable = new CompositeDisposable();
            
            _references.Mediator.PauseButton.onClick.AddListener(OnPauseButtonClicked);

            IsOpened.Value = true;
        }

        public override void Close()
        {
            _references.Mediator.PauseButton.onClick.RemoveAllListeners();
            _references.Mediator.Slider.onValueChanged.RemoveAllListeners();
            
            _references.ToyRotatorElement?.Dispose();
            _compositeDisposable?.Dispose();
            
            IsOpened.Value = false;
            
            Object.Destroy(_references.Mediator.gameObject);
        }

        private void OnPauseButtonClicked()
        {
            _windowService.OpenAsync<PauseWindow>().Forget();
        }
    }
}