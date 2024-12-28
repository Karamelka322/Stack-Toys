using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready
{
    public interface ICompanySceneReadyObserver : ISceneReadyObserver
    {
        
    }

    public interface ISceneReadyObserver
    {
        BoolReactiveProperty IsReady { get; }
    }
}