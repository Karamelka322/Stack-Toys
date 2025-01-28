using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Formulas;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyDragState : BaseState
    {
        private const float Smooth = 15f;
        
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IInputService _inputService;
        private readonly ToyMediator _toyMediator;
        private readonly IClickFormulas _clickFormulas;
        private readonly Camera _camera;

        private Vector3 _offset;

        public ToyDragState(ToyMediator toyMediator, IInputService inputService,
            ILevelBorderSystem levelBorderSystem, IClickFormulas clickFormulas)
        {
            _clickFormulas = clickFormulas;
            _levelBorderSystem = levelBorderSystem;
            _camera = Camera.main;
            _toyMediator = toyMediator;
            _inputService = inputService;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyDragState> { }

        public override void Enter()
        {
            var clickToWorldPosition = _clickFormulas.ClickToWorldPosition(_camera,
                Input.mousePosition, _toyMediator.transform);
            
            _offset = clickToWorldPosition - _toyMediator.transform.position;

            _inputService.OnClick += OnClick;
        }

        public override void Exit()
        {
            _inputService.OnClick -= OnClick;
        }

        private async void OnClick(Vector3 clickPosition)
        {
            var clickToWorldPosition = _clickFormulas.ClickToWorldPosition(_camera,
                Input.mousePosition, _toyMediator.transform);
            
            var worldPosition = clickToWorldPosition - _offset;
            var clampPosition = await _levelBorderSystem.ClampAsync(_toyMediator, worldPosition);
            
            _toyMediator.transform.position = Vector3.Lerp(
                _toyMediator.transform.position, clampPosition, Time.deltaTime * Smooth);
        }
    }
}