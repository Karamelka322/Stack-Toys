using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Babble
{
    public class BabbleFactory : IBabbleFactory
    {
        private readonly IAssetServices _assetServices;

        public BabbleFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }
        
        public async UniTask<GameObject> SpawnAsync(Vector3 position, Transform parent)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.ToyBabble);
            var babble = Object.Instantiate(prefab, position, Quaternion.identity, parent);
            
            return babble;
        }
    }
}