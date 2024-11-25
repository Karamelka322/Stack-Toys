using CodeBase.UI.General.Mediators.Windows.Loading;

namespace CodeBase.UI.Interfaces.General.Factories.Windows.Loading
{
    public interface ILoadingWindowFactory
    {
        LoadingWindowMediator Spawn();
    }
}