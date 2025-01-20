using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects
{
    public interface IToyChoicerProvider
    {
        ReactiveCollection<(ToyChoicerMediator, ToyChoicerStateMachine)> ToyChoicers { get; }
        
        void Register(ToyChoicerMediator mediator, ToyChoicerStateMachine stateMachine);
        void Unregister(ToyChoicerMediator mediator, ToyChoicerStateMachine stateMachine);
    }
}