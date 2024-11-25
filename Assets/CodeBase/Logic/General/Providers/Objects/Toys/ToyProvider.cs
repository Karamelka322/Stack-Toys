using System;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;

namespace CodeBase.Logic.General.Providers.Objects.Toys
{
    public class ToyProvider : IToyProvider, IDisposable
    {
        private readonly IToyFactory _toyFactory;

        public ReactiveCollection<(ToyMediator, ToyStateMachine)> Toys { get; }
        
        public ToyProvider(IToyFactory toyFactory)
        {
            Toys = new ReactiveCollection<(ToyMediator, ToyStateMachine)>();
            
            _toyFactory = toyFactory;
            _toyFactory.OnSpawn += OnSpawn;
        }
        
        public void Dispose()
        {
            Toys?.Dispose();
            _toyFactory.OnSpawn -= OnSpawn;
        }
        
        private void OnSpawn(ToyMediator mediator, ToyStateMachine stateMachine)
        {
            Toys.Add((mediator, stateMachine));
        }
    }
}