using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Scenes.Infinity.Windows.Main
{
    public interface IInfinityMainWindow : IMainWindow
    {
        BoolReactiveProperty IsOpened { get; }
        UniTask OpenAsync();
    }
}