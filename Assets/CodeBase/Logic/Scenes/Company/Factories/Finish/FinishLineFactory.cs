using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Finish;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Factories.Finish
{
    public class FinishLineFactory : BaseLineFactory, IFinishLineFactory
    {
        private readonly IAssetService _assetService;

        public FinishLineFactory(IAssetService assetService, ILevelSizeSystem levelSizeSystem) : base(levelSizeSystem)
        {
            _assetService = assetService;
        }

        public async UniTask<FinishLineMediator> SpawnAsync(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.CompanyScene.FinishLine);
            var mediator = Object.Instantiate(prefab, position, rotation).GetComponent<FinishLineMediator>();
            
            await ScaleAsync(mediator.HeightRectTransform, mediator.Line);
            
            return mediator;
        }
    }
}