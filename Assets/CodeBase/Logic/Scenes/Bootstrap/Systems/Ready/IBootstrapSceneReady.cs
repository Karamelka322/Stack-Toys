using UniRx;

namespace CodeBase.Logic.Scenes.Bootstrap.Systems.Ready
{
    public interface IBootstrapSceneReady
    {
        BoolReactiveProperty IsReady { get; }
    }
}