using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.Unity.Toys;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyEndDragTransition : BaseTransition
    {
        private readonly ToyMediator _toyMediator;
        private readonly IInputService _inputService;
        private readonly IRaycastCommand _raycastCommand;

        public ToyEndDragTransition(ToyMediator toyMediator, IInputService inputService, IRaycastCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyEndDragTransition> { }

        public override void Enter()
        {
            _inputService.OnClickUp += OnClickUp;
        }

        public override void Exit()
        {
            _inputService.OnClickUp -= OnClickUp;
        }

        private void OnClickUp(Vector3 clickPosition)
        {
            if (_raycastCommand.HasUI(clickPosition) == false)
            {
                IsCompleted.Value = true;
            }
        }
    }
}