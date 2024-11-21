using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindow : ICompanyMainWindow
    {
        private readonly ICompanyMainWindowFactory _companyMainWindowFactory;
        private readonly IToySelectObserver _toySelectObserver;

        private CompanyMainWindowMediator _mediator;

        public event Action<float> OnSliderChanged;
        
        public CompanyMainWindow(ICompanyMainWindowFactory companyMainWindowFactory, IToySelectObserver toySelectObserver)
        {
            _toySelectObserver = toySelectObserver;
            _companyMainWindowFactory = companyMainWindowFactory;
        }

        public async UniTask OpenAsync()
        {
            _mediator = await _companyMainWindowFactory.SpawnAsync();
            _mediator.Slider.onValueChanged.AddListener(OnSliderChangedInvoke);

            _toySelectObserver.Toy.Subscribe(OnSelectableToyChanged);

            HideSlider();
        }

        public void Close()
        {
            _mediator.Slider.onValueChanged.RemoveAllListeners();
            Object.Destroy(_mediator.gameObject);
        }

        public float GetSliderValue()
        {
            return _mediator.Slider.value;
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

        private void OnSliderChangedInvoke(float value) => OnSliderChanged?.Invoke(value);
    }
}