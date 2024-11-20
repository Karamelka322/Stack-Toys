using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Unity.Toys;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyTowerState : BaseState
    {
        private readonly ToyMediator _toyMediator;

        public ToyTowerState(ToyMediator toyMediator)
        {
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyTowerState> { }

        public override void Enter()
        {
            _toyMediator.gameObject.AddComponent<Rigidbody>();
        }

        public override void Exit()
        {
            
        }
    }
}