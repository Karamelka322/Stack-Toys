using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public interface ICompanyMainWindowFactory
    {
        UniTask<CompanyMainWindowMediator> SpawnAsync();
    }
}