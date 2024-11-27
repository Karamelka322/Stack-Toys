using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;

namespace CodeBase.Logic.General.Providers.Objects.Toys
{
    public class ToyProvider : IToyProvider, IDisposable
    {
        public ReactiveCollection<(ToyMediator, ToyStateMachine)> Toys { get; }
        
        public ToyProvider()
        {
            Toys = new ReactiveCollection<(ToyMediator, ToyStateMachine)>();
        }

        public void Register(ToyMediator mediator, ToyStateMachine stateMachine)
        {
            Toys.Add((mediator, stateMachine));
        }

        public void Unregister(ToyMediator mediator, ToyStateMachine stateMachine)
        {
            Toys.Remove((mediator, stateMachine));
        }

        public void Dispose()
        {
            Toys?.Dispose();
        }
    }
}