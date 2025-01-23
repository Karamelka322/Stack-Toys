using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.StateMachines.Toys.Transitions;
using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys
{
    public class ToyStateMachine2 : ToyStateMachine
    {
        private readonly ToyMediator _toyMediator;
        
        private readonly ToyRotateState.Factory _rotateStateFactory;
        private readonly ToyDragState.Factory _dragStateFactory;
        private readonly ToyTowerState.Factory _towerStartFactory;
        private readonly ToyIdleState.Factory _idleStateFactory;

        private readonly ToyStartDragTransition.Factory _startDragTransitionFactory;
        private readonly ClickUpTransition.Factory _clickUpTransitionFactory;
        private readonly ToyTowerTransition.Factory _towerTransitionFactory;
        private readonly ToyRotationTransition.Factory _toyRotationTransitionFactory;

        public ToyStateMachine2(
            ToyMediator toyMediator, 
            ToyBabbleState.Factory toyBabbleStateFactory, 
            ToyRotateState.Factory toyRotateStateFactory, 
            ToyDragState.Factory toyDragStateFactory, 
            ToyTowerState.Factory towerStartFactory, 
            ToyIdleState.Factory idleStateFactory, 
            ToyRotationTransition.Factory toyRotationTransitionFactory, 
            ToySelectTransition.Factory toySelectTransitionFactory, 
            ToyStartDragTransition.Factory toyStartDragTransitionFactory, 
            ClickUpTransition.Factory toyClickUpTransitionFactory, 
            ToyTowerTransition.Factory toyTowerTransitionFactory) : base(toyMediator, toyBabbleStateFactory, 
            toyRotateStateFactory, toyDragStateFactory, towerStartFactory, idleStateFactory, 
            toyRotationTransitionFactory, toySelectTransitionFactory, toyStartDragTransitionFactory, 
            toyClickUpTransitionFactory, toyTowerTransitionFactory)
        {
            _idleStateFactory = idleStateFactory;
            _toyRotationTransitionFactory = toyRotationTransitionFactory;
            _towerTransitionFactory = toyTowerTransitionFactory;
            _towerStartFactory = towerStartFactory;
            _clickUpTransitionFactory = toyClickUpTransitionFactory;
            _startDragTransitionFactory = toyStartDragTransitionFactory;
            _dragStateFactory = toyDragStateFactory;
            _rotateStateFactory = toyRotateStateFactory;
            _toyMediator = toyMediator;
            
            Launch();
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyStateMachine2> { }
        
        protected override StateTree InstallStateTree()
        {
            var rotateState = _rotateStateFactory.Create(_toyMediator);
            var dragState = _dragStateFactory.Create(_toyMediator);
            var towerState = _towerStartFactory.Create(_toyMediator);
            var idleState = _idleStateFactory.Create(_toyMediator);
            
            var startDragTransition = _startDragTransitionFactory.Create(_toyMediator);
            var clickUpTransition = _clickUpTransitionFactory.Create(_toyMediator);
            var towerTransition = _towerTransitionFactory.Create();
            var rotationTransition = _toyRotationTransitionFactory.Create(_toyMediator);
            
            var tree = new StateTree();
            
            tree.RegisterState(idleState);
            tree.RegisterTransition(idleState, rotationTransition, rotateState);
            tree.RegisterTransition(idleState, startDragTransition, dragState);
            tree.RegisterTransition(idleState, towerTransition, towerState);
            
            tree.RegisterState(dragState);
            tree.RegisterTransition(dragState, clickUpTransition, idleState);
            
            tree.RegisterState(rotateState);
            tree.RegisterTransition(rotateState, clickUpTransition, idleState);
            
            tree.RegisterState(towerState);
            
            return tree;
        }
    }
}