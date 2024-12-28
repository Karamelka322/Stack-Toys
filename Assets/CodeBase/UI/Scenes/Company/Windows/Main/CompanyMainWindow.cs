using System;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Main;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindow : BaseWindow, ICompanyMainWindow
    {
        private const float _sliderDuration = 0.5f;
        
        private readonly ICompanyMainWindowFactory _companyMainWindowFactory;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly IWindowService _windowService;
        private readonly IToyCountObserver _toyCountObserver;

        private CompositeDisposable _compositeDisposable;
        private CompanyMainWindowMediator _mediator;

        public BoolReactiveProperty IsOpened { get; }
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

            IsOpened = new BoolReactiveProperty();
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

            IsOpened.Value = true;
        }

        public override void Close()
        {
            _mediator.PauseButton.onClick.RemoveAllListeners();
            _mediator.Slider.onValueChanged.RemoveAllListeners();
            
            _compositeDisposable?.Dispose();
            
            IsOpened.Value = false;
            
            Object.Destroy(_mediator.gameObject);
        }

        public float GetSliderValue()
        {
            return _mediator.Slider.value;
        }
        
        public Quaternion SliderValueToRotation(float value)
        {
            return Quaternion.Euler(0, 0, 360 * value);
        }

        private void OnPauseButtonClicked()
        {
            _windowService.OpenAsync<PauseWindow>().Forget();
        }

        private void OnSelectableToyChanged(ToyMediator toyMediator)
        {
            if (toyMediator == null)
            {
                if (_toyCountObserver.NumberOfTowerBuildToys.Value == 0)
                {
                    HideSlider(0);
                    SetRandomValueToSlider();
                }
                else
                {
                    HideSlider(_sliderDuration, SetRandomValueToSlider);
                }
            }
            else
            {
                ShowSlider();
            }
        }

        private void SetRandomValueToSlider()
        {
            _mediator.Slider.value = UnityEngine.Random.Range(0f, 1f);
        }
        
        private void ShowSlider()
        {
            DOVirtual.Float(0f, 1f, _sliderDuration, 
                (value) => _mediator.SliderCanvasGroup.alpha = value);
        }

        private void HideSlider(float duration = _sliderDuration, TweenCallback onComplete = null)
        {
            DOVirtual.Float(1f, 0f, duration, 
                (value) => _mediator.SliderCanvasGroup.alpha = value).OnComplete(onComplete);
        }
        
        private void UpdateToyCounter(int value)
        {
            _mediator.ToyCounter.text = $"{value}";
        }

        private void OnSliderChangedInvoke(float value) => OnSliderChanged?.Invoke(value);
    }
}