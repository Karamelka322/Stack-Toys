using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Factories.Toys
{
    public class ToySelectEffectFactory : IToySelectEffectFactory
    {
        private readonly IAssetService _assetService;

        public ToySelectEffectFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<GameObject> SpawnAsync(Transform parent)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(
                AddressableConstants.CompanyScene.ToySelectEffect);

            return Object.Instantiate(prefab, parent);
        }
    }
}