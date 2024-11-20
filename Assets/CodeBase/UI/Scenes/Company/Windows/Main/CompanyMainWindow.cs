using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindow : ICompanyMainWindow
    {
        private readonly ICompanyMainWindowFactory _companyMainWindowFactory;
        
        private CompanyMainWindowMediator _mediator;

        public event Action<float> OnSliderChanged;
        
        public CompanyMainWindow(ICompanyMainWindowFactory companyMainWindowFactory)
        {
            _companyMainWindowFactory = companyMainWindowFactory;
        }

        public async UniTask OpenAsync()
        {
            _mediator = await _companyMainWindowFactory.SpawnAsync();
            _mediator.Slider.onValueChanged.AddListener(OnSliderChangedInvoke);

            HideSlider();
        }
        
        public void Close()
        {
            _mediator.Slider.onValueChanged.RemoveAllListeners();
            Object.Destroy(_mediator.gameObject);
        }

        public void ShowSlider()
        {
            _mediator.Slider.gameObject.SetActive(true);
        }

        public void HideSlider()
        {
            _mediator.Slider.gameObject.SetActive(false);
        }
        
        private void OnSliderChangedInvoke(float value) => OnSliderChanged?.Invoke(value);
    }
}