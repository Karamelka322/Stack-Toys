using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using Zenject;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class CompletedMenuLevelElement
    {
        private readonly ISceneLoadService _sceneLoadService;

        public CompletedMenuLevelElement(MenuLevelElementMediator mediator, int levelIndex, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            
            mediator.Button.onClick.AddListener(OnClick);
            mediator.Label.text = (levelIndex + 1).ToString();
        }
        
        public class Factory : PlaceholderFactory<MenuLevelElementMediator, int, CompletedMenuLevelElement> { }

        private void OnClick()
        {
            _sceneLoadService.LoadScene(SceneNames.Company);
        }
    }
}