using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Interfaces.Scenes.Company.Windows.Main
{
    public interface ICompanyMainWindow : IMainWindow
    {
        BoolReactiveProperty IsOpened { get; }
        
        UniTask OpenAsync();
        void Close();
    }
}