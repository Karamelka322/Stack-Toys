using System;
using System.Threading.Tasks;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class LevelBorderSystem : IDisposable, ILevelBorderSystem
    {
        private const float TopBorder = 7.5f;
        
        private readonly ICameraSettingsProvider _cameraSettingsProvider;
        private readonly ILevelProvider _levelProvider;
        private readonly IDisposable _disposable;

        private LevelMediator _level;

        public Vector3 BottomLeftPoint { get; private set; }
        public Vector3 BottomRightPoint { get; private set; }
        public Vector3 TopLeftPoint { get; private set; }
        public Vector3 TopRightPoint { get; private set; }
        
        public LevelBorderSystem(
            ICompanySceneLoad companySceneLoad,
            ILevelProvider levelProvider,
            ICameraSettingsProvider cameraSettingsProvider)
        {
            _cameraSettingsProvider = cameraSettingsProvider;
            _levelProvider = levelProvider;

            _disposable = companySceneLoad.IsLoaded.Subscribe(OnSceneLoad);
        }

        private void OnSceneLoad(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }
        
            _level = _levelProvider.Level;
            
            BottomLeftPoint = _level.OriginPoint.position - _level.OriginPoint.right * _level.Width / 2f;
            BottomRightPoint = _level.OriginPoint.position + _level.OriginPoint.right * _level.Width / 2f;
        
            TopLeftPoint = BottomLeftPoint + _level.OriginPoint.up * _level.Height;
            TopRightPoint = BottomRightPoint + _level.OriginPoint.up * _level.Height;
        }

        public Vector3 Clamp(ToyMediator toyMediator, Vector3 position)
        {
            var clampPosition = position;
            var size = toyMediator.MeshRenderer.bounds.size;
            var max = Mathf.Max(size.x, size.y) / 2f;

            clampPosition.x = Mathf.Clamp(clampPosition.x, BottomLeftPoint.x + max, BottomRightPoint.x - max);
            clampPosition.y = Mathf.Clamp(clampPosition.y, 
                _level.OriginPoint.position.y + max, (_level.OriginPoint.up * _level.Height).y - max + TopBorder);
            
            return clampPosition;
        }

        public async UniTask<Vector3> GetCameraStartPointAsync()
        {
            var offset = await _cameraSettingsProvider.GetOffsetAsync();
            var position = _level.OriginPoint.position + offset;
            
            return position;
        }

        public async UniTask<Vector3> GetCameraEndPointAsync()
        {
            var startPosition = await GetCameraStartPointAsync();
            var endPosition = startPosition;
            
            endPosition.y = Mathf.Max(startPosition.y, _level.Height);

            return endPosition;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}