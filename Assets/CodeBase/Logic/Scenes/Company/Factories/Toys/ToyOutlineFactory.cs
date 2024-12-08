using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Factories.Toys
{
    public class ToyOutlineFactory : IToyOutlineFactory
    {
        private readonly IAssetServices _assetServices;

        public ToyOutlineFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<GameObject> SpawnAsync()
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.ToyShadow);
            var shadow = Object.Instantiate(prefab);
            
            return shadow;
        }
    }
}