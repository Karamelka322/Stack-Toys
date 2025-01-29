using CodeBase.Logic.Scenes.Company.Systems.Finish;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Lines
{
    public interface IFinishLineProvider
    {
        ReactiveProperty<FinishLine> Line { get; }
        
        void Register(FinishLine finishLineMediator);
    }
}