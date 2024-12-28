using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Factories.Levels
{
    public class InfinityLevelFactory : IInfinityLevelFactory
    {
        private readonly IAssetServices _assetServices;

        public InfinityLevelFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<LevelMediator> SpawnAsync()
        {
            var addressableName = AddressableConstants.InfinityScene.Level;
            var prefab = await _assetServices.LoadAsync<GameObject>(addressableName);
            var level = Object.Instantiate(prefab).GetComponent<LevelMediator>();

            return level;
        }
    }
}