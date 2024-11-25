using CodeBase.Logic.General.Services.Windows;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Services.Windows
{
    public interface IWindowService
    {
        void RegisterWindow<TWindow>(TWindow window) where TWindow : BaseWindow;
        UniTask OpenAsync<TWindow>() where TWindow : BaseWindow;
        UniTask CloseAsync<TWindow>() where TWindow : BaseWindow;
    }
}