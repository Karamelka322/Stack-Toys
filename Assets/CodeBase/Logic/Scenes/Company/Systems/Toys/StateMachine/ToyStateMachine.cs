using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine
{
    public class ToyStateMachine : BaseStateMachine
    {
        private readonly ToyMediator _toyMediator;
        
        private readonly ToyRotateState.Factory _rotateStateFactory;
        private readonly ToyBabbleState.Factory _babbleStateFactory;
        private readonly ToyDragState.Factory _dragStateFactory;
        private readonly ToyTowerState.Factory _towerStartFactory;

        private readonly ToySelectTransition.Factory _selectTransitionFactory;
        private readonly ToyStartDragTransition.Factory _startDragTransitionFactory;
        private readonly ToyEndDragTransition.Factory _endDragTransitionFactory;
        private readonly ToyTowerTransition.Factory _towerTransitionFactory;

        public ToyStateMachine(ToyMediator toyMediator, 
            ToyBabbleState.Factory toyBabbleStateFactory,
            ToyRotateState.Factory toyRotateStateFactory,
            ToyDragState.Factory toyDragStateFactory,
            ToyTowerState.Factory towerStartFactory,
            ToySelectTransition.Factory toySelectTransitionFactory,
            ToyStartDragTransition.Factory toyStartDragTransitionFactory,
            ToyEndDragTransition.Factory toyEndDragTransitionFactory,
            ToyTowerTransition.Factory toyTowerTransitionFactory)
        {
            _towerTransitionFactory = toyTowerTransitionFactory;
            _towerStartFactory = towerStartFactory;
            _endDragTransitionFactory = toyEndDragTransitionFactory;
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
            
            var selectTransition = _selectTransitionFactory.Create(_toyMediator);
            var startDragTransition = _startDragTransitionFactory.Create(_toyMediator);
            var endDragTransition = _endDragTransitionFactory.Create(_toyMediator);
            var towerTransition = _towerTransitionFactory.Create();
            
            var tree = new StateTree();
            
            tree.RegisterState(babbleState);
            tree.RegisterTransition(babbleState, selectTransition, rotateState);
            
            tree.RegisterState(rotateState);
            tree.RegisterTransition(rotateState, startDragTransition, dragState);
            tree.RegisterTransition(rotateState, towerTransition, towerState);
            
            tree.RegisterState(dragState);
            tree.RegisterTransition(dragState, endDragTransition, rotateState);
            
            tree.RegisterState(towerState);
            
            return tree;
        }
    }
}