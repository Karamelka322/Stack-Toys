using CodeBase.Data.Constants;
using CodeBase.Data.Models.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels
{
    public class LevelsConfigProvider : ILevelsConfigProvider
    {
        private readonly IAssetServices _assetServices;
        private readonly AsyncLazy _prepareResourcesTask;
        
        private LevelsConfig _config;

        public LevelsConfigProvider(IAssetServices assetServices)
        {
            _assetServices = assetServices;
            _prepareResourcesTask = UniTask.Lazy(PrepareResources);
        }

        public async UniTask<GameObject> GetLevelPrefabAsync(int index)
        {
            await _prepareResourcesTask;

            var assetReferenceGameObject = _config.Levels[index].LevelAsset;
            var prefab = await _assetServices.LoadAsync(assetReferenceGameObject);
            
            return prefab;
        }
        
        public async UniTask<GameObject> GetToyPrefabAsync()
        {
            await _prepareResourcesTask;

            var assetReferenceGameObject = _config.Levels[0].ToyAssets[0];
            var prefab = await _assetServices.LoadAsync(assetReferenceGameObject);
            
            return prefab;
        }

        private async UniTask PrepareResources()
        {
            _config = await _assetServices.LoadAsync<LevelsConfig>(AddressableNames.LevelsConfig);
        }
    }
}