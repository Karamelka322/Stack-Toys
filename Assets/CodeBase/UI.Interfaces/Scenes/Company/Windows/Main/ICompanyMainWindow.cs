using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public interface ICompanyMainWindow
    {
        event Action<float> OnSliderChanged;
        
        UniTask OpenAsync();
        void Close();
        void ShowSlider();
        void HideSlider();
        float GetSliderValue();
    }
}