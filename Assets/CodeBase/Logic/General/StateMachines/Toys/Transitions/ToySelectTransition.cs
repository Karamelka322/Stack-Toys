using System;
using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.Transitions
{
    public class ToySelectTransition : BaseTransition
    {
        private const float DistanceToSelect = 1.2f;

        private readonly IClickCommand _clickCommand;
        private readonly IInputService _inputService;
        private readonly ToyMediator _toyMediator;
        private readonly Camera _camera;

        private RaycastHit _raycastHit;

        public ToySelectTransition(ToyMediator toyMediator, IInputService inputService, IClickCommand clickCommand)
        {
            _clickCommand = clickCommand;
            _camera = Camera.main;
            _inputService = inputService;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToySelectTransition> { }

        public override void Enter()
        {
            _inputService.OnClickDown += OnClickDown;
        }

        public override void Exit()
        {
            _inputService.OnClickDown -= OnClickDown;
        }

        private void OnClickDown(Vector3 mousePosition)
        {
            if (_clickCommand.HasUI(mousePosition))
            {
                return;
            }
            
            var screenToWorldPoint = _camera.ScreenToWorldPoint(mousePosition);
            screenToWorldPoint.z = _toyMediator.transform.position.z;
            
            if (Vector3.Distance(screenToWorldPoint, _toyMediator.transform.position) < DistanceToSelect)
            {
                IsCompleted.Value = true;
            }
        }
    }

    public class ToySelectSystem : IDisposable
    {
        private readonly IInputService _inputService;

        public ToySelectSystem(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnClickDown += OnClickDown;
        }

        public void Dispose()
        {
            _inputService.OnClickDown -= OnClickDown;
        }

        public void Track()
        {
            
        }

        private void OnClickDown(Vector3 clickPosition)
        {
            
        }
    }
}