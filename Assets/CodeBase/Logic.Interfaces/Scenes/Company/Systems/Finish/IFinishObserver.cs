using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish
{
    public interface IFinishObserver
    {
        BoolReactiveProperty IsFinished { get; }
    }
}