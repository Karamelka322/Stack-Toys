using CodeBase.Data.Constants;
using CodeBase.Data.ScriptableObjects.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels
{
    public class CompanyLevelsSettingProvider : ICompanyLevelsSettingProvider
    {
        private readonly IAssetServices _assetServices;
        private readonly AsyncLazy _prepareResourcesTask;
        
        private CompanyLevelsSettings _config;
        
        public CompanyLevelsSettingProvider(IAssetServices assetServices)
        {
            _assetServices = assetServices;
            _prepareResourcesTask = UniTask.Lazy(PrepareResources);
        }

        public async UniTask<GameObject> GetLevelPrefabAsync(int index)
        {
            await _prepareResourcesTask;

            var assetReferenceGameObject = _config.Levels[index].EnvironmentAsset;
            var prefab = await _assetServices.LoadAsync(assetReferenceGameObject);
            
            return prefab;
        }

        public async UniTask<int> GetNumberOfLevelsAsync()
        {
            await _prepareResourcesTask;
            
            return _config.Levels.Length;
        }

        public async UniTask<float> GetLevelHeightAsync(int index)
        {
            await _prepareResourcesTask;

            return _config.Levels[index].Height;
        }
        
        public async UniTask<GameObject[]> GetToyPrefabsAsync(int levelIndex)
        {
            await _prepareResourcesTask;

            var assetReferenceGameObject = _config.Levels[levelIndex].ToyAssets;
            var prefab = await _assetServices.LoadAsync(assetReferenceGameObject);
            
            return prefab;
        }

        private async UniTask PrepareResources()
        {
            _config = await _assetServices.LoadAsync<CompanyLevelsSettings>(AddressableConstants.LevelsConfig);
        }
    }
}