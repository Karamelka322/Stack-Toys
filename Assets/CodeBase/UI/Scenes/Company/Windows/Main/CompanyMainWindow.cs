using System;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Main;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindow : BaseWindow, ICompanyMainWindow
    {
        private readonly ICompanyMainWindowFactory _companyMainWindowFactory;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly IWindowService _windowService;
        private readonly IToyCountObserver _toyCountObserver;

        private CompositeDisposable _compositeDisposable;
        private CompanyMainWindowMediator _mediator;

        public event Action<float> OnSliderChanged;
        
        public CompanyMainWindow(
            ICompanyMainWindowFactory companyMainWindowFactory,
            IToySelectObserver toySelectObserver,
            IToyCountObserver toyCountObserver,
            IWindowService windowService) : base(windowService)
        {
            _toyCountObserver = toyCountObserver;
            _windowService = windowService;
            _toySelectObserver = toySelectObserver;
            _companyMainWindowFactory = companyMainWindowFactory;
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _companyMainWindowFactory.SpawnAsync();

            _compositeDisposable = new CompositeDisposable();
            
            _mediator.PauseButton.onClick.AddListener(OnPauseButtonClicked);
            _mediator.Slider.onValueChanged.AddListener(OnSliderChangedInvoke);

            _toyCountObserver.LeftAvailableNumberOfToys.Subscribe(UpdateToyCounter).AddTo(_compositeDisposable);
            _toySelectObserver.Toy.Subscribe(OnSelectableToyChanged).AddTo(_compositeDisposable);
            
            HideSlider();
        }

        public override void Close()
        {
            _mediator.PauseButton.onClick.RemoveAllListeners();
            _mediator.Slider.onValueChanged.RemoveAllListeners();
            
            _compositeDisposable?.Dispose();
            
            Object.Destroy(_mediator.gameObject);
        }

        public float GetSliderValue()
        {
            return _mediator.Slider.value;
        }
        
        private void OnPauseButtonClicked()
        {
            _windowService.OpenAsync<PauseWindow>().Forget();
        }

        private void OnSelectableToyChanged(ToyMediator toyMediator)
        {
            if (toyMediator == null)
            {
                HideSlider();
            }
            else
            {
                ShowSlider();
            }
        }
        
        private void ShowSlider()
        {
            _mediator.Slider.value = 0;
            _mediator.Slider.gameObject.SetActive(true);
        }

        private void HideSlider()
        {
            _mediator.Slider.gameObject.SetActive(false);
        }
        
        private void UpdateToyCounter(int value)
        {
            _mediator.ToyCounter.text = $"{value}";
        }

        private void OnSliderChangedInvoke(float value) => OnSliderChanged?.Invoke(value);
    }
}