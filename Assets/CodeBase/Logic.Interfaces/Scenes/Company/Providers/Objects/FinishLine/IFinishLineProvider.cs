using CodeBase.Logic.General.Unity.Finish;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.FinishLine
{
    public interface IFinishLineProvider
    {
        void Register(FinishLineMediator finishLineMediator);
        ReactiveProperty<FinishLineMediator> FinishLine { get; }
    }
}