using CodeBase.Data.Constants;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Factories.Finish
{
    public class FinishLineFactory : IFinishLineFactory
    {
        private readonly IAssetServices _assetServices;

        public FinishLineFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<FinishLineMediator> SpawnAsync(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.FinishLine);
            var mediator = Object.Instantiate(prefab, position, rotation).GetComponent<FinishLineMediator>();
            
            return mediator;
        }
    }
}