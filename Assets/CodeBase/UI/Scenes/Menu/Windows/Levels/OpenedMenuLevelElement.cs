using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using Zenject;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class OpenedMenuLevelElement
    {
        private readonly MenuLevelElementMediator _mediator;
        private readonly ISceneLoadService _sceneLoadService;

        public OpenedMenuLevelElement(MenuLevelElementMediator mediator, int levelIndex, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _mediator = mediator;
            
            _mediator.Button.onClick.AddListener(OnClick);
            _mediator.Label.text = (levelIndex + 1).ToString();
        }
        
        public class Factory : PlaceholderFactory<MenuLevelElementMediator, int, OpenedMenuLevelElement> { }
        
        private void OnClick()
        {
            _sceneLoadService.LoadScene(SceneNames.Company);
        }
    }
}