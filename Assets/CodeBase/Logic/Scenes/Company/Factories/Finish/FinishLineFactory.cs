using System.Threading.Tasks;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Finish;
using CodeBase.Logic.Scenes.Company.Systems.Finish;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Factories.Finish
{
    public class FinishLineFactory : BaseLineFactory, IFinishLineFactory
    {
        private readonly IAssetService _assetService;
        private readonly FinishLine.Factory _factory;

        public FinishLineFactory(
            IAssetService assetService,
            ILevelSizeSystem levelSizeSystem, 
            FinishLine.Factory factory) : base(levelSizeSystem)
        {
            _factory = factory;
            _assetService = assetService;
        }

        public async UniTask<FinishLine> SpawnAsync(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.CompanyScene.FinishLine);
            var mediator = Object.Instantiate(prefab, position, rotation).GetComponent<FinishLineMediator>();
            
            await ScaleAsync(mediator.HeightRectTransform, mediator.Line);
            
            return _factory.Create(mediator);
        }
    }
}