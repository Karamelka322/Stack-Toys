using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Factories.Confetti;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Confetti
{
    public class ConfettiEffectFactory : IConfettiEffectFactory
    {
        private readonly IAssetService _assetService;

        public ConfettiEffectFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<GameObject> SpawnAsync(Vector3 position, Quaternion rotation)
        {
            var addressableName = AddressableConstants.ConfettiEffect;
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);
            
            var effect = Object.Instantiate(prefab, position, rotation);
            
            Object.Destroy(effect, 10f);
            
            return effect;
        }
    }
}