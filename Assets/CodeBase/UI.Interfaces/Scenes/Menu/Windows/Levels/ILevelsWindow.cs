using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Menu.Windows.Levels
{
    public interface ILevelsWindow
    {
        UniTask OpenAsync();
        void Close();
    }
}