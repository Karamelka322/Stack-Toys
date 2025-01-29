using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Lines;
using CodeBase.Logic.Scenes.Company.Systems.Finish;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Providers.Objects.Lines
{
    public class FinishLineProvider : IFinishLineProvider
    {
        public ReactiveProperty<FinishLine> Line { get; }

        public FinishLineProvider()
        {
            Line = new ReactiveProperty<FinishLine>();
        }

        public void Register(FinishLine finishLineMediator)
        {
            Line.Value = finishLineMediator;
        }
    }
}