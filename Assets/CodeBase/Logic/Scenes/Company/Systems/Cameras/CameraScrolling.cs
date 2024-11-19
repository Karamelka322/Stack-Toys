using System;
using CodeBase.Logic.General.Providers.ScriptableObjects.Cameras;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.Scenes.Company.Providers;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras
{
    public class CameraScrolling : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly ILevelProvider _levelProvider;
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private float _interpolation;

        public CameraScrolling(
            ICompanySceneLoad sceneLoad,
            ILevelProvider levelProvider,
            IInputService inputService,
            ICameraSettingsProvider cameraSettingsProvider)
        {
            _cameraSettingsProvider = cameraSettingsProvider;
            _inputService = inputService;
            _levelProvider = levelProvider;
            _camera = Camera.main;
            
            _disposable = sceneLoad.IsLoaded.Subscribe(OnSceneLoaded);
        }
        
        private void OnSceneLoaded(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }

            _inputService.OnSwipe += OnSwipe;

            _camera.UpdateVolumeStack();

            SetStartPosition();
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

        public void Dispose()
        {
            _disposable?.Dispose();
            _inputService.OnSwipe += OnSwipe;
        }
    }
}