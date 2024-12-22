using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Finish;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Finish;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Finish;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Scenes.Company.Windows.Finish
{
	public class CompanyFinishWindow : BaseWindow, ICompanyFinishWindow
	{
	    private readonly ICompanyFinishWindowFactory _finishWindowFactory;
	    private readonly ISceneLoadService _sceneLoadService;
	    private readonly ICompanySceneUnload _companySceneUnload;
	    private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

	    private CompanyFinishWindowMediator _mediator;

	    public CompanyFinishWindow(
		    ICompanyFinishWindowFactory finishWindowFactory,
		    ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
		    IWindowService windowService,
		    ICompanySceneUnload companySceneUnload,
		    ISceneLoadService sceneLoadService) : base(windowService)
	    {
		    _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
		    _companySceneUnload = companySceneUnload;
		    _sceneLoadService = sceneLoadService;
		    _finishWindowFactory = finishWindowFactory;
	    }

	    public override async UniTask OpenAsync()
	    {
		    _mediator = await _finishWindowFactory.SpawnAsync();
		    _mediator.NextLevelButton.onClick.AddListener(OnClickNextLevelButton);
	    }

	    private void OnClickNextLevelButton()
	    {
		    _mediator.NextLevelButton.onClick.RemoveAllListeners();

		    var nextLevelIndex = _companyLevelsSaveDataProvider.GetNextLevelIndex();
		    
		    _companyLevelsSaveDataProvider.SetTargetLevel(nextLevelIndex);
		    _companyLevelsSaveDataProvider.SetCurrentLevel(nextLevelIndex);
		    
		    _companySceneUnload.Unload();
		    _sceneLoadService.ReloadSceneAsync(1f).Forget();
	    }
	}
}