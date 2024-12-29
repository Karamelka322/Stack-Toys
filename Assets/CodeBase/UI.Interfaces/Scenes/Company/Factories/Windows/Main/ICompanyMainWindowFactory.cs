using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.RuntimeData;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main
{
    public interface ICompanyMainWindowFactory
    {
        UniTask<CompanyMainWindowReferences> SpawnAsync();
    }
}