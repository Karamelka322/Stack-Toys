using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Factories.Toys
{
    public class ToyShadowFactory : IToyShadowFactory
    {
        private readonly IAssetService _assetService;

        public ToyShadowFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<GameObject> SpawnAsync()
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.ToyShadow);
            var shadow = Object.Instantiate(prefab);
            
            return shadow;
        }
    }
}