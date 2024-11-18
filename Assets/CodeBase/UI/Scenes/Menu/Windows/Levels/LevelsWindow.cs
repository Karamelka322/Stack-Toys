using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.Services.SceneLoad;
using CodeBase.UI.Interfaces.Scenes.Menu.Factories.Levels;
using CodeBase.UI.Interfaces.Scenes.Menu.Windows.Levels;
using CodeBase.UI.Scenes.Menu.Mediators.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class LevelsWindow : ILevelsWindow
    {
        private readonly ILevelsWindowFactory _levelsWindowFactory;
        private readonly ISceneLoadService _sceneLoadService;

        private LevelsWindowMediator _mediator;

        public LevelsWindow(ILevelsWindowFactory levelsWindowFactory, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _levelsWindowFactory = levelsWindowFactory;
        }

        public async UniTask OpenAsync()
        {
            _mediator = await _levelsWindowFactory.SpawnAsync();
            
            _mediator.OpenNextSceneButton.onClick.AddListener(OnOpenNextSceneButtonClicked);
        }

        public void Close()
        {
            Object.Destroy(_mediator.gameObject);
        }

        private void OnOpenNextSceneButtonClicked()
        {
            _mediator.OpenNextSceneButton.onClick.RemoveAllListeners();

            _sceneLoadService.LoadScene(SceneNames.Company);
        }
    }
}