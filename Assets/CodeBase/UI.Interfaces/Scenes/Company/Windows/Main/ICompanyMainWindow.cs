using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Interfaces.Scenes.Company.Windows.Main
{
    public interface ICompanyMainWindow
    {
        event Action<float> OnSliderChanged;
        BoolReactiveProperty IsOpened { get; }

        UniTask OpenAsync();
        void Close();
        float GetSliderValue();
    }
}