using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Ready
{
    public interface ICompanySceneReadyObserver : ISceneReadyObserver
    {
        
    }

    public interface ISceneReadyObserver
    {
        BoolReactiveProperty IsReady { get; }
    }
}