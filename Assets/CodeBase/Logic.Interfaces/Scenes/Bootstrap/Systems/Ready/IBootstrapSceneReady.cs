using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Bootstrap.Systems.Ready
{
    public interface IBootstrapSceneReady
    {
        BoolReactiveProperty IsReady { get; }
    }
}