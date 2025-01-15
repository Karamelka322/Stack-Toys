using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class OpenedMenuLevelElement
    {
        private readonly MenuLevelElementMediator _mediator;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly int _levelIndex;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;

        public OpenedMenuLevelElement(MenuLevelElementMediator mediator, int levelIndex, 
            ISceneLoadService sceneLoadService, ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _sceneLoadService = sceneLoadService;
            _mediator = mediator;
            _levelIndex = levelIndex;
            
            _mediator.Button.onClick.AddListener(OnClick);
            _mediator.Label.text = (levelIndex + 1).ToString();
        }
        
        public class Factory : PlaceholderFactory<MenuLevelElementMediator, int, OpenedMenuLevelElement> { }
        
        private void OnClick()
        {
            _companyLevelsSaveDataProvider.SetCurrentLevel(_levelIndex);
            
            _mediator.Button.onClick.RemoveAllListeners();
            _sceneLoadService.LoadSceneAsync(SceneNames.Company, 1f).Forget();
        }
    }
}