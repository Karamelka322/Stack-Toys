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
    public class ToyStartDragTransition : BaseTransition
    {
        private readonly ToyMediator _toyMediator;
        private readonly IInputService _inputService;
        private readonly IClickCommand _raycastCommand;

        private bool _isFirstClickOnUi;

        public ToyStartDragTransition(ToyMediator toyMediator, IInputService inputService, IClickCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyStartDragTransition> { }

        public override void Enter()
        {
            _inputService.OnClickDown += OnClickDown;
            _inputService.OnSwipe += OnSwipe;
        }

        public override void Exit()
        {
            _inputService.OnClickDown -= OnClickDown;
            _inputService.OnSwipe -= OnSwipe;
        }

        private void OnClickDown(Vector3 obj)
        {
            _isFirstClickOnUi = _raycastCommand.HasUI(Input.mousePosition);
        }

        private void OnSwipe(Vector3 swipeDirection)
        {
            if (swipeDirection != Vector3.zero && _isFirstClickOnUi == false)
            {
                IsCompleted.Value = true;
                _inputService.OnSwipe -= OnSwipe;
            }
        }
    }
}