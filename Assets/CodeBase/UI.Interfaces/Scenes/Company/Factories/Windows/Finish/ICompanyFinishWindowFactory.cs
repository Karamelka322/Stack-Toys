using CodeBase.UI.Scenes.Company.Mediators.Windows.Finish;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Finish
{
    public interface ICompanyFinishWindowFactory
    {
        UniTask<CompanyFinishWindowMediator> SpawnAsync();
    }
}