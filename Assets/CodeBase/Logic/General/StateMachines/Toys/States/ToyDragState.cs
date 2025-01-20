using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyDragState : BaseState
    {
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IInputService _inputService;
        private readonly ToyMediator _toyMediator;
        private readonly Camera _camera;

        private Vector3 _offset;

        public ToyDragState(ToyMediator toyMediator, IInputService inputService, ILevelBorderSystem levelBorderSystem)
        {
            _levelBorderSystem = levelBorderSystem;
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

        private async void OnClick(Vector3 clickPosition)
        {
            var worldPosition = ClickToWorldPosition(clickPosition) - _offset;
            var clampPosition = await _levelBorderSystem.ClampAsync(_toyMediator, worldPosition);
            
            _toyMediator.transform.position = clampPosition;
        }

        private Vector3 ClickToWorldPosition(Vector3 clickPosition)
        {
            var ray = _camera.ScreenPointToRay(clickPosition);
            
            var distance = Vector3.Distance(ray.origin, _toyMediator.transform.position);
            var worldPositiom = ray.origin + ray.direction * distance;
            worldPositiom.z = _toyMediator.transform.position.z;
            
            UnityEngine.Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
            
            return worldPositiom;
        }
    }
}