using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Levels
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

        public async UniTask<GameObject> GetLevelPrefabAsync()
        {
            await _prepareResourcesTask;

            var prefab = await _assetServices.LoadAsync(_config.LevelPrefab);
            
            return prefab;
        }

        private async UniTask PrepareResources()
        {
            _config = await _assetServices.LoadAsync<LevelsConfig>(AddressableNames.LevelsConfig);
        }
    }
}