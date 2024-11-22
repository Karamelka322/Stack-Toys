using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Finish;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Finish;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Finish;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Windows.Finish
{
	public class CompanyFinishWindow : ICompanyFinishWindow
	{
	    private readonly ICompanyFinishWindowFactory _finishWindowFactory;
	    
	    private CompanyFinishWindowMediator _mediator;

	    public CompanyFinishWindow(ICompanyFinishWindowFactory finishWindowFactory)
	    {
		    _finishWindowFactory = finishWindowFactory;
	    }

	    public async UniTask OpenAsync()
	    {
		    _mediator = await _finishWindowFactory.SpawnAsync();
	    }
    }
}