using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.RuntimeData;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindow : BaseWindow, ICompanyMainWindow
    {
        private readonly ICompanyMainWindowFactory _companyMainWindowFactory;
        private readonly IWindowService _windowService;
        private readonly IToyCountObserver _toyCountObserver;

        private CompositeDisposable _compositeDisposable;
        private CompanyMainWindowReferences _references;

        public ToyRotatorElement ToyRotatorElement => _references.ToyRotatorElement;
        public BoolReactiveProperty IsOpened { get; }
        
        public CompanyMainWindow(
            ICompanyMainWindowFactory companyMainWindowFactory,
            IToyCountObserver toyCountObserver,
            IWindowService windowService) : base(windowService)
        {
            _toyCountObserver = toyCountObserver;
            _windowService = windowService;
            _companyMainWindowFactory = companyMainWindowFactory;

            IsOpened = new BoolReactiveProperty();
        }

        public override async UniTask OpenAsync()
        {
            _references = await _companyMainWindowFactory.SpawnAsync();

            _compositeDisposable = new CompositeDisposable();
            
            _references.Mediator.PauseButton.onClick.AddListener(OnPauseButtonClicked);
            _toyCountObserver.LeftAvailableNumberOfToys.Subscribe(UpdateToyCounter).AddTo(_compositeDisposable);

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
            // _windowService.OpenAsync<PauseWindow>().Forget();
        }

        private void UpdateToyCounter(int value)
        {
            _references.Mediator.ToyCounter.text = $"{value}";
        }
    }
}