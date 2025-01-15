using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Interfaces.Scenes.Menu.Windows.Menu
{
    public interface IMenuWindow
    {
        BoolReactiveProperty IsShowing { get; }
        
        UniTask OpenAsync();
    }
}