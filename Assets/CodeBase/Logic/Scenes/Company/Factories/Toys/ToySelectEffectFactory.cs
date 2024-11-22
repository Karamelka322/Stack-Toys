using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Factories.Toys
{
    public class ToySelectEffectFactory : IToySelectEffectFactory
    {
        private readonly IAssetServices _assetServices;

        public ToySelectEffectFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<GameObject> SpawnAsync(Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(
                AddressableNames.CompanyScene.ToySelectEffect);

            return Object.Instantiate(prefab, parent);
        }
    }
}