using CodeBase.UI.General.Mediators.Windows.Pause;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.General.Factories.Windows.Pause
{
    public interface IPauseWindowFactory
    {
        UniTask<PauseWindowMediator> SpawnAsync();
    }
}