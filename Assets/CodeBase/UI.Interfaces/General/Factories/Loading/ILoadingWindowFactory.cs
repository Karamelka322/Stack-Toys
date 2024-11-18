using CodeBase.UI.General.Mediators.Loading;

namespace CodeBase.UI.Interfaces.General.Factories.Loading
{
    public interface ILoadingWindowFactory
    {
        LoadingWindowMediator Spawn();
    }
}