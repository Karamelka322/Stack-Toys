using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Factories.Levels
{
    public class InfinityLevelFactory : IInfinityLevelFactory
    {
        private readonly IAssetService _assetService;

        public InfinityLevelFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<LevelMediator> SpawnAsync()
        {
            var addressableName = AddressableConstants.InfinityScene.Level;
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);
            var level = Object.Instantiate(prefab).GetComponent<LevelMediator>();

            return level;
        }
    }
}