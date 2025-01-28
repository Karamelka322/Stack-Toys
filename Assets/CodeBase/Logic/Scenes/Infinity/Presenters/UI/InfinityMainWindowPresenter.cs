using CodeBase.UI.Scenes.Infinity.Windows.Main;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Infinity.Presenters.UI
{
    public class InfinityMainWindowPresenter
    {
        public InfinityMainWindowPresenter(IInfinityMainWindow infinityMainWindow)
        {
            infinityMainWindow.OpenAsync().Forget();
        }
    }
}