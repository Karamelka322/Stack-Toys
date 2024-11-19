using CodeBase.Logic.General.Unity.Toys;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyDragState
    {
        public class Factory : PlaceholderFactory<ToyMediator, ToyDragState> { }
    }
}