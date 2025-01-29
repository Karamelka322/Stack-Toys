using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using UniRx;

namespace CodeBase.Logic.Scenes.Bootstrap.Systems.Ready
{
    public class BootstrapSceneReadyObserver : ISceneReadyObserver
    {
        public BoolReactiveProperty IsReady { get; }
        
        public BootstrapSceneReadyObserver()
        {
            IsReady = new BoolReactiveProperty(true);
        }
    }
}