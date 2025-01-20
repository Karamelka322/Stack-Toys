using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.ToyChoicer
{
    public class ToyChoicerStateMachine : BaseStateMachine
    {
        private readonly ToyChoicerMediator _choicer;
        private readonly ToyMediator _toy1;
        private readonly ToyMediator _toy2;
        
        private readonly ToyChoicerRotateState.Factory _rotateStateFactory;

        public ToyChoicerStateMachine(
            ToyChoicerMediator choicer,
            ToyMediator toy1,
            ToyMediator toy2,
            ToyChoicerRotateState.Factory rotateStateFactory)
        {
            _rotateStateFactory = rotateStateFactory;
            _toy2 = toy2;
            _toy1 = toy1;
            _choicer = choicer;
        }

        public class Factory : PlaceholderFactory<ToyChoicerMediator, ToyMediator, ToyMediator, ToyChoicerStateMachine> { }

        protected override StateTree InstallStateTree()
        {
            var tree = new StateTree();
            
            var rotateState = _rotateStateFactory.Create(_choicer, _toy1, _toy2);

            tree.RegisterState(rotateState);
            
            return tree;
        }
    }
}