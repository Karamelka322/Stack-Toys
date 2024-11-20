using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyTowerTransition : BaseTransition
    {
        private readonly IRaycastCommand _raycastCommand;
        private readonly IInputService _inputService;

        private bool _isSwipe;

        public ToyTowerTransition(IInputService inputService, IRaycastCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
        }
        
        public class Factory : PlaceholderFactory<ToyTowerTransition> { }

        public override void Enter()
        {
            _isSwipe = false;
            
            _inputService.OnClickUp += OnClickUp;
            _inputService.OnSwipe += OnSwipe;
        }

        public override void Exit()
        {
            _inputService.OnClickUp -= OnClickUp;
            _inputService.OnSwipe -= OnSwipe;
        }

        private void OnSwipe(Vector3 obj)
        {
            _isSwipe = true;
        }

        private void OnClickUp(Vector3 clickPosition)
        {
            if (_isSwipe == false && _raycastCommand.HasUI(clickPosition) == false)
            {
                IsCompleted.Value = true;
            }
        }
    }
}