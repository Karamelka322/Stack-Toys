using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Lines;
using CodeBase.Logic.Scenes.Company.Factories.Finish;
using CodeBase.Logic.Scenes.Infinity.Objects.Lines;
using CodeBase.Logic.Scenes.Infinity.Unity.Lines;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Factories.Lines
{
    public class RecordLineFactory : BaseLineFactory, IRecordLineFactory
    {
        private readonly IAssetService _assetService;
        private readonly RecordLine.Factory _factory;

        public RecordLineFactory(
            IAssetService assetService, 
            ILevelSizeSystem levelSizeSystem, 
            RecordLine.Factory factory) : base(levelSizeSystem)
        {
            _factory = factory;
            _assetService = assetService;
        }

        public async UniTask<RecordLine> SpawnAsync(Vector3 position, string titleLocalizationId, float height)
        {
            var addressableName = AddressableConstants.InfinityScene.RecordLine;
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);

            var lineMediator = Object.Instantiate(prefab, position,
                Quaternion.identity).GetComponent<RecordLineMediator>();
            
            var lineLogic = _factory.Create(lineMediator);

            lineLogic.SetTitleAsync(titleLocalizationId).Forget();
            lineLogic.SetHeightAsync(height).Forget();
            
            await ScaleAsync(lineMediator.HeightRectTransform, lineMediator.Line);
            
            return lineLogic;
        }
    }
}