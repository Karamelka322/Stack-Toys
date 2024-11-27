using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyEndDragTransition : BaseTransition
    {
        private readonly ToyMediator _toyMediator;
        private readonly IInputService _inputService;
        private readonly IClickCommand _raycastCommand;

        public ToyEndDragTransition(ToyMediator toyMediator, IInputService inputService, IClickCommand raycastCommand)
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