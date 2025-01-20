using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.StateMachines.Toys.Transitions;
using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys
{
    public class ToyStateMachine : BaseStateMachine
    {
        private readonly ToyMediator _toyMediator;
        
        private readonly ToyRotateState.Factory _rotateStateFactory;
        private readonly ToyBabbleState.Factory _babbleStateFactory;
        private readonly ToyDragState.Factory _dragStateFactory;
        private readonly ToyTowerState.Factory _towerStartFactory;
        private readonly ToyIdleState.Factory _idleStateFactory;

        private readonly ToySelectTransition.Factory _selectTransitionFactory;
        private readonly ToyStartDragTransition.Factory _startDragTransitionFactory;
        private readonly ClickUpTransition.Factory _clickUpTransitionFactory;
        private readonly ToyTowerTransition.Factory _towerTransitionFactory;
        private readonly ToyRotationTransition.Factory _toyRotationTransitionFactory;
        
        public ToyStateMachine(
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
            ToyTowerTransition.Factory toyTowerTransitionFactory)
        {
            _idleStateFactory = idleStateFactory;
            _toyRotationTransitionFactory = toyRotationTransitionFactory;
            _towerTransitionFactory = toyTowerTransitionFactory;
            _towerStartFactory = towerStartFactory;
            _clickUpTransitionFactory = toyClickUpTransitionFactory;
            _startDragTransitionFactory = toyStartDragTransitionFactory;
            _dragStateFactory = toyDragStateFactory;
            _rotateStateFactory = toyRotateStateFactory;
            _selectTransitionFactory = toySelectTransitionFactory;
            _babbleStateFactory = toyBabbleStateFactory;
            _toyMediator = toyMediator;

            Launch();
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyStateMachine> { }
        
        protected override StateTree InstallStateTree()
        {
            var babbleState = _babbleStateFactory.Create(_toyMediator);
            var rotateState = _rotateStateFactory.Create(_toyMediator);
            var dragState = _dragStateFactory.Create(_toyMediator);
            var towerState = _towerStartFactory.Create(_toyMediator);
            var idleState = _idleStateFactory.Create(_toyMediator);
            
            var selectTransition = _selectTransitionFactory.Create(_toyMediator);
            var startDragTransition = _startDragTransitionFactory.Create(_toyMediator);
            var clickUpTransition = _clickUpTransitionFactory.Create(_toyMediator);
            var towerTransition = _towerTransitionFactory.Create();
            var rotationTransition = _toyRotationTransitionFactory.Create(_toyMediator);
            
            var tree = new StateTree();
            
            tree.RegisterState(babbleState);
            tree.RegisterTransition(babbleState, selectTransition, idleState);

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