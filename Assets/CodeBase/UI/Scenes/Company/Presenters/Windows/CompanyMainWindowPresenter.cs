using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Presenters.Windows
{
    public class CompanyMainWindowPresenter
    {
        public CompanyMainWindowPresenter(ICompanyMainWindow companyMainWindow)
        {
            companyMainWindow.OpenAsync().Forget();
        }
    }
}