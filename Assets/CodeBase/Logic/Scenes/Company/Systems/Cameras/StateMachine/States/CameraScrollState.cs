using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States
{
    public class CameraScrollState : BaseState
    {
        private readonly IInputService _inputService;
        private readonly ILevelProvider _levelProvider;
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly Camera _camera;

        private float _interpolation;

        public CameraScrollState(
            Camera camera,
            IInputService inputService,
            ILevelProvider levelProvider,
            ICameraSettingsProvider cameraSettingsProvider)
        {
            _camera = camera;
            _cameraSettingsProvider = cameraSettingsProvider;
            _levelProvider = levelProvider;
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
        
        private void SetStartPosition()
        {
            _camera.transform.position = _levelProvider.Level.CameraStartPoint.position;
        }

        private async void OnSwipe(Vector3 direction)
        {
            var startPosition = _levelProvider.Level.CameraStartPoint.position;
            var endPosition = _levelProvider.Level.CameraEndPoint.position;
            var speed = await _cameraSettingsProvider.GetScrollingSpeedAsync();

            var nextInterpolation = _interpolation - direction.y * (Time.deltaTime * speed);
            
            _interpolation = Mathf.Clamp01(nextInterpolation);
            _camera.transform.position = Vector3.Lerp(startPosition, endPosition, _interpolation);
        }
    }
}