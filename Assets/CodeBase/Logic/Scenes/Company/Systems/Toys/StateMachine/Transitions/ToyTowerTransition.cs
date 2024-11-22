using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyTowerTransition : BaseTransition
    {
        private readonly IRaycastCommand _raycastCommand;
        private readonly IInputService _inputService;
        
        private bool _isClickPressed;

        public ToyTowerTransition(IInputService inputService, IRaycastCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
        }
        
        public class Factory : PlaceholderFactory<ToyTowerTransition> { }

        public override void Enter()
        {
            _isClickPressed = _inputService.IsClickPressed;
            _inputService.OnClickUp += OnClickUp;
        }

        public override void Exit()
        {
            _inputService.OnClickUp -= OnClickUp;
        }

        private void OnClickUp(Vector3 clickPosition)
        {
            if (_isClickPressed == false && _raycastCommand.HasUI(clickPosition) == false)
            {
                IsCompleted.Value = true;
            }
            
            if (_isClickPressed)
            {
                _isClickPressed = false;
            }
        }
    }
}