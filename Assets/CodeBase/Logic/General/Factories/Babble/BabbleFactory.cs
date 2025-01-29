using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Factories.Babble;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Babble
{
    public class BabbleFactory : IBabbleFactory
    {
        private readonly IAssetService _assetService;

        public BabbleFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<GameObject> SpawnAsync(ToyMediator toyMediator)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.ToyBabble);
            
            var parent = toyMediator.transform;
            var position = parent.position;
            
            var babble = Object.Instantiate(prefab, position, Quaternion.identity);

            babble.transform.localScale = Vector3.one * (toyMediator.BabbleSize * 2f);
            babble.transform.parent = parent;

            return babble;
        }
    }
}