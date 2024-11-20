using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindowPresenter
    {
        public CompanyMainWindowPresenter(ICompanyMainWindow companyMainWindow)
        {
            companyMainWindow.OpenAsync().Forget();
        }
    }
}