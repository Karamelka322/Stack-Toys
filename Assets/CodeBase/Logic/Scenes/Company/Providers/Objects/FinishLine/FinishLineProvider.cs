using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.FinishLine;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Providers.Objects.FinishLine
{
    public class FinishLineProvider : IFinishLineProvider
    {
        public ReactiveProperty<FinishLineMediator> FinishLine { get; private set; }

        public FinishLineProvider()
        {
            FinishLine = new ReactiveProperty<FinishLineMediator>();
        }

        public void Register(FinishLineMediator finishLineMediator)
        {
            FinishLine.Value = finishLineMediator;
        }
    }
}