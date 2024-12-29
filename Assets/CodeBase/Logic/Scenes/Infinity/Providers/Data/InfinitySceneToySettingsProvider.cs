using CodeBase.Data.General.Constants;
using CodeBase.Data.Scenes.Infinity.ScriptableObjects.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Data;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Scenes.Infinity.Providers.Data
{
    public class InfinitySceneToySettingsProvider : IInfinitySceneToySettingsProvider
    {
        private readonly IAssetService _assetService;
        private readonly AsyncLazy _prepareResourcesTask;
        
        private InfinitySceneToySettings _config;

        public InfinitySceneToySettingsProvider(IAssetService assetService)
        {
            _assetService = assetService;
            _prepareResourcesTask = UniTask.Lazy(PrepareResourcesAsync);
        }

        public async UniTask<AssetReferenceGameObject[]> GetToysAsync()
        {
            await _prepareResourcesTask;

            return _config.Toys;
        }

        private async UniTask PrepareResourcesAsync()
        {
            var addressableName = AddressableConstants.InfinityScene.ToysSettings;
            _config = await _assetService.LoadAsync<InfinitySceneToySettings>(addressableName);
        }
    }
}