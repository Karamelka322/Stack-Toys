using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Factories.Windows.Main
{
    public interface IInfinityMainWindowFactory
    {
        UniTask<InfinityMainWindowReferences> SpawnAsync();
    }
}