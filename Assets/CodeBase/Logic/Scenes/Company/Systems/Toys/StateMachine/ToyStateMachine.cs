using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine
{
    public class ToyStateMachine : BaseStateMachine
    {
        private readonly ToyMediator _toyMediator;
        
        private readonly ToyRotateState.Factory _toyRotateStateFactory;
        private readonly ToyBabbleState.Factory _toyBabbleStateFactory;
        
        private readonly ToySelectTransition.Factory _toySelectTransitionFactory;

        public ToyStateMachine(ToyMediator toyMediator, 
            ToyBabbleState.Factory toyBabbleStateFactory,
            ToyRotateState.Factory toyRotateStateFactory,
            ToySelectTransition.Factory toySelectTransitionFactory)
        {
            _toyRotateStateFactory = toyRotateStateFactory;
            _toySelectTransitionFactory = toySelectTransitionFactory;
            _toyBabbleStateFactory = toyBabbleStateFactory;
            _toyMediator = toyMediator;
            
            Launch();
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyStateMachine> { }
        
        protected override StateTree InstallStateTree()
        {
            var babbleState = _toyBabbleStateFactory.Create(_toyMediator);
            var rotateState = _toyRotateStateFactory.Create(_toyMediator);
            
            var selectTransition = _toySelectTransitionFactory.Create(_toyMediator);
            
            var tree = new StateTree();
            
            tree.RegisterState(babbleState);
            tree.RegisterState(rotateState);
            
            tree.RegisterTransition(babbleState, selectTransition, rotateState);
            
            return tree;
        }
    }
}