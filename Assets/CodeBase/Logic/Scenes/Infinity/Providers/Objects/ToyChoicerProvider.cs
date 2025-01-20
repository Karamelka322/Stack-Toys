using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Providers.Objects
{
    public class ToyChoicerProvider : IToyChoicerProvider
    {
        public ReactiveCollection<(ToyChoicerMediator, ToyChoicerStateMachine)> ToyChoicers { get; }

        public ToyChoicerProvider()
        {
            ToyChoicers = new ReactiveCollection<(ToyChoicerMediator, ToyChoicerStateMachine)>();
        }

        public void Register(ToyChoicerMediator mediator, ToyChoicerStateMachine stateMachine)
        {
            ToyChoicers.Add((mediator, stateMachine));
        }

        public void Unregister(ToyChoicerMediator mediator, ToyChoicerStateMachine stateMachine)
        {
            ToyChoicers.Remove((mediator, stateMachine));
        }
    }
}