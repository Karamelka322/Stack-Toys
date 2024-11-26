using CodeBase.Data.Constants;
using CodeBase.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Cameras
{
    public class CameraSettingsProvider : ICameraSettingsProvider
    {
        private readonly IAssetServices _assetServices;
        private readonly AsyncLazy _prepareResourcesTask;
        
        private CameraSettings _config;

        public CameraSettingsProvider(IAssetServices assetServices)
        {
            _assetServices = assetServices;

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
            _config = await _assetServices.LoadAsync<CameraSettings>(AddressableNames.CameraSettings);
        }
    }
}