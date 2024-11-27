using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;

namespace CodeBase.Logic.Interfaces.General.Providers.Objects.Toys
{
    public interface IToyProvider
    {
        ReactiveCollection<(ToyMediator, ToyStateMachine)> Toys { get; }
        void Dispose();
        void Register(ToyMediator mediator, ToyStateMachine stateMachine);
        void Unregister(ToyMediator mediator, ToyStateMachine stateMachine);
    }
}