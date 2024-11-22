using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Company.Windows.Main
{
    public interface ICompanyMainWindow
    {
        event Action<float> OnSliderChanged;
        
        UniTask OpenAsync();
        void Close();
        float GetSliderValue();
    }
}