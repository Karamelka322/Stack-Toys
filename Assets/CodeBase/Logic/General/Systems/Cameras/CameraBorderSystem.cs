using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras
{
    public class CameraBorderSystem : ICameraBorderSystem
    {
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly Camera _camera;
        private readonly ILevelProvider _levelProvider;

        public CameraBorderSystem(
            ICameraSettingsProvider cameraSettingsProvider,
            ILevelProvider levelProvider,
            ILevelBorderSystem levelBorderSystem)
        {
            _levelBorderSystem = levelBorderSystem;
            _levelProvider = levelProvider;
            _camera = Camera.main;
            _cameraSettingsProvider = cameraSettingsProvider;
        }

        public async UniTask<Vector3> GetCameraStartPointAsync()
        {
            var offset = await _cameraSettingsProvider.GetOffsetAsync();
            var position = _levelProvider.Level.Value.OriginPoint.position + offset;
            
            return position;
        }

        public async UniTask<Vector3> GetCameraEndPointAsync()
        {
            var startPosition = await GetCameraStartPointAsync();
            var levelHeight = await _levelBorderSystem.GetHeightAsync();
            var endPosition = startPosition;
            
            endPosition.y = Mathf.Max(startPosition.y, levelHeight);

            return endPosition;
        }

        public async UniTask<float> GetInterpolationAsync()
        {
            var startPosition = await GetCameraStartPointAsync();
            var endPosition = await GetCameraEndPointAsync();

            var maxDistance = startPosition != endPosition ? Vector3.Distance(startPosition, endPosition) : 1;
            return Vector3.Distance(startPosition, _camera.transform.position) / maxDistance;
        }
    }
}