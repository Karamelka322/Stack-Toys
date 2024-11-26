using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States
{
    public class CameraScrollState : BaseState
    {
        private readonly IInputService _inputService;
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly Camera _camera;

        private float _interpolation;

        public CameraScrollState(
            Camera camera,
            IInputService inputService,
            ILevelBorderSystem levelBorderSystem,
            ICameraSettingsProvider cameraSettingsProvider)
        {
            _levelBorderSystem = levelBorderSystem;
            _camera = camera;
            _cameraSettingsProvider = cameraSettingsProvider;
            _inputService = inputService;
        }
        
        public class Factory :PlaceholderFactory<Camera, CameraScrollState> { }

        public override void Enter()
        {
            SetStartPosition();
            
            _inputService.OnSwipe += OnSwipe;
        }

        public override void Exit()
        {
            _inputService.OnSwipe -= OnSwipe;
        }
        
        private async void SetStartPosition()
        {
            _camera.transform.position = await _levelBorderSystem.GetCameraStartPointAsync();
        }

        private async void OnSwipe(Vector3 direction)
        {
            var startPosition = await _levelBorderSystem.GetCameraStartPointAsync();
            var endPosition = await _levelBorderSystem.GetCameraEndPointAsync();
            var speed = await _cameraSettingsProvider.GetScrollingSpeedAsync();

            var distance = startPosition != endPosition ? Vector3.Distance(startPosition, endPosition) : 1;
            var nextInterpolation = _interpolation - direction.y * (Time.deltaTime * speed) / distance;
            
            _interpolation = Mathf.Clamp01(nextInterpolation);
            _camera.transform.position = Vector3.Lerp(startPosition, endPosition, _interpolation);
        }
    }
}