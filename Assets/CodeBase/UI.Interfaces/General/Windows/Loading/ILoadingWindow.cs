using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.General.Windows.Loading
{
    public interface ILoadingWindow
    {
        void Open();
        void Close();
        UniTask HideAsync();
        UniTask ShowAsync();
    }
}