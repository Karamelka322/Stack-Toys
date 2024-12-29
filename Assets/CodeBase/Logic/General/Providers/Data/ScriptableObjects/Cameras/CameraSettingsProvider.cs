using CodeBase.Data.General.Constants;
using CodeBase.Data.General.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Cameras
{
    public class CameraSettingsProvider : ICameraSettingsProvider
    {
        private readonly IAssetService _assetService;
        private readonly AsyncLazy _prepareResourcesTask;
        
        private CameraSettings _config;

        public CameraSettingsProvider(IAssetService assetService)
        {
            _assetService = assetService;

            _prepareResourcesTask = UniTask.Lazy(PrepareResourcesAsync);
        }

        public async UniTask<float> GetScrollingSpeedAsync()
        {
            await _prepareResourcesTask;

            return _config.ScrollingSpeed;
        }
        
        public async UniTask<Vector3> GetOffsetAsync()
        {
            await _prepareResourcesTask;

            return _config.Offset;
        }

        private async UniTask PrepareResourcesAsync()
        {
            _config = await _assetService.LoadAsync<CameraSettings>(AddressableConstants.CameraSettings);
        }
    }
}