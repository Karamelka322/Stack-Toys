using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.Transitions
{
    public class ClickUpTransition : BaseTransition
    {
        private readonly ToyMediator _toyMediator;
        private readonly IInputService _inputService;
        private readonly IClickCommand _raycastCommand;

        public ClickUpTransition(ToyMediator toyMediator, IInputService inputService, IClickCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ClickUpTransition> { }

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
            IsCompleted.Value = true;
        }
    }
}