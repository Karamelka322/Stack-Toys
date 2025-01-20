using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Scenes.Company.Systems.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States
{
    public class CameraScrollState : BaseState
    {
        private readonly IInputService _inputService;
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly Camera _camera;
        private readonly ICameraBorderSystem _cameraBorderSystem;

        public CameraScrollState(
            Camera camera,
            IInputService inputService,
            ICameraBorderSystem cameraBorderSystem,
            ICameraSettingsProvider cameraSettingsProvider)
        {
            _cameraBorderSystem = cameraBorderSystem;
            _camera = camera;
            _cameraSettingsProvider = cameraSettingsProvider;
            _inputService = inputService;

            SetStartPosition();
        }
        
        public class Factory :PlaceholderFactory<Camera, CameraScrollState> { }

        public override void Enter()
        {
            // SetStartPosition();
            
            _inputService.OnSwipe += OnSwipe;
        }

        public override void Exit()
        {
            _inputService.OnSwipe -= OnSwipe;
        }
        
        private async void SetStartPosition()
        {
            _camera.transform.position = await _cameraBorderSystem.GetCameraStartPointAsync();
        }

        private async void OnSwipe(Vector3 direction)
        {
            var startPosition = await _cameraBorderSystem.GetCameraStartPointAsync();
            var endPosition = await _cameraBorderSystem.GetCameraEndPointAsync();
            var speed = await _cameraSettingsProvider.GetScrollingSpeedAsync();

            var distance = startPosition != endPosition ? Vector3.Distance(startPosition, endPosition) : 1;
            var interpolation = await _cameraBorderSystem.GetInterpolationAsync();
            var nextInterpolation = interpolation - direction.y * (Time.deltaTime * speed) / distance;
            nextInterpolation = Mathf.Clamp01(nextInterpolation);
            
            _camera.transform.position = Vector3.Lerp(startPosition, endPosition, nextInterpolation);
        }
    }
}