using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToySelectTransition : BaseTransition
    {
        private const float DistanceToSelect = 1f;
        
        private readonly IInputService _inputService;
        private readonly ToyMediator _toyMediator;
        private readonly Camera _camera;
        
        private RaycastHit _raycastHit;

        public ToySelectTransition(ToyMediator toyMediator, IInputService inputService)
        {
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
            var screenToWorldPoint = _camera.ScreenToWorldPoint(mousePosition);
            screenToWorldPoint.z = _toyMediator.transform.position.z;

            if (Vector3.Distance(screenToWorldPoint, _toyMediator.transform.position) < DistanceToSelect)
            {
                IsCompleted.Value = true;
            }
        }
    }
}