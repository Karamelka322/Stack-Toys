using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish
{
    public interface IFinishObserver
    {
        BoolReactiveProperty IsFinished { get; }
    }
}