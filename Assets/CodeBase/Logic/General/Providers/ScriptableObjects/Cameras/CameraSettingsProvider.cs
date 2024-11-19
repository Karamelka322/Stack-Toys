using CodeBase.Data.Constants;
using CodeBase.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.Interfaces.Services.Assets;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.General.Providers.ScriptableObjects.Cameras
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

        private async UniTask PrepareResourcesAsync()
        {
            _config = await _assetServices.LoadAsync<CameraSettings>(AddressableNames.CameraSettings);
        }
    }
}