using System;
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
        private const float ScaleFactory = 1.3f;
        
        private readonly IAssetService _assetService;

        public BabbleFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<GameObject> SpawnAsync(ToyMediator toyMediator)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.ToyBabble);
            
            var size = toyMediator.MeshRenderer.bounds.size;
            var max = Math.Max(size.x, size.y) * ScaleFactory;
            var scale = new Vector3(max, max, max);
            
            var parent = toyMediator.transform;
            var position = parent.position;
            
            var babble = Object.Instantiate(prefab, position, Quaternion.identity);
            
            babble.transform.localScale = scale;
            babble.transform.parent = parent;
            
            return babble;
        }
    }
}