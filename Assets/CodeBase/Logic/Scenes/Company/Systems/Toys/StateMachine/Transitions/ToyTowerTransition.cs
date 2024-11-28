using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyTowerTransition : BaseTransition
    {
        private readonly IClickCommand _raycastCommand;
        private readonly IInputService _inputService;
        
        private bool _isBlock;
        
        public ToyTowerTransition(IInputService inputService, IClickCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
        }
        
        public class Factory : PlaceholderFactory<ToyTowerTransition> { }

        public override void Enter()
        {
            _isBlock = _inputService.IsClickPressed;
            
            _inputService.OnSwipe += OnSwipe;
            _inputService.OnClickUp += OnClickUp;
        }

        public override void Exit()
        {
            _inputService.OnSwipe -= OnSwipe;
            _inputService.OnClickUp -= OnClickUp;
        }

        private void OnSwipe(Vector3 clickPosition)
        {
            _isBlock = true;
        }

        private void OnClickUp(Vector3 clickPosition)
        {
            if (_isBlock == false && _raycastCommand.HasUI(clickPosition) == false)
            {
                IsCompleted.Value = true;
            }

            _isBlock = false;
        }
    }
}