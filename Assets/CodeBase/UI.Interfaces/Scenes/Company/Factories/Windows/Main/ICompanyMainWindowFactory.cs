using CodeBase.UI.Scenes.Company.Mediators.Windows.Main;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main
{
    public interface ICompanyMainWindowFactory
    {
        UniTask<CompanyMainWindowMediator> SpawnAsync();
    }
}