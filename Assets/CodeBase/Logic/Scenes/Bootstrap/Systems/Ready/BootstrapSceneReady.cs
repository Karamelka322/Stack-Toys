using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UniRx;

namespace CodeBase.Logic.Scenes.Bootstrap.Systems.Ready
{
    public class BootstrapSceneReady : IBootstrapSceneReady
    {
        public BoolReactiveProperty IsReady { get; }
        
        public BootstrapSceneReady(ISceneLoadService sceneLoadService)
        {
            IsReady = new BoolReactiveProperty(true);

            sceneLoadService.LoadScene(SceneNames.Menu);
        }
    }
}