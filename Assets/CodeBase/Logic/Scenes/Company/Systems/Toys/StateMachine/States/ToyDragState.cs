using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.Unity.Toys;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyDragState : BaseState
    {
        private readonly IInputService _inputService;
        private readonly ToyMediator _toyMediator;
        private readonly Camera _camera;
        
        private Vector3 _offset;

        public ToyDragState(ToyMediator toyMediator, IInputService inputService)
        {
            _camera = Camera.main;
            _toyMediator = toyMediator;
            _inputService = inputService;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyDragState> { }

        public override void Enter()
        {
            _offset = ClickToWorldPosition(Input.mousePosition) - _toyMediator.transform.position;
            
            _inputService.OnClick += OnClick;
        }

        public override void Exit()
        {
            _inputService.OnClick -= OnClick;
        }

        private void OnClick(Vector3 clickPosition)
        {
            _toyMediator.transform.position = ClickToWorldPosition(clickPosition) - _offset;
        }

        private Vector3 ClickToWorldPosition(Vector3 clickPosition)
        {
            var ray = _camera.ScreenPointToRay(clickPosition);
            
            var distance = Vector3.Distance(ray.origin, _toyMediator.transform.position);
            var nextPosition = ray.origin + ray.direction * distance;
            nextPosition.z = _toyMediator.transform.position.z;
            
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
            
            return nextPosition;
        }
    }
}