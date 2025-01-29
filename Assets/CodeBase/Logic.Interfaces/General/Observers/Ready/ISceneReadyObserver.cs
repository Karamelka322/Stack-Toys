using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready
{
    public interface ISceneReadyObserver
    {
        BoolReactiveProperty IsReady { get; }
    }
}