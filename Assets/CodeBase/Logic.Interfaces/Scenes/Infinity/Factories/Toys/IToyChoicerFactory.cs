using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys
{
    public interface IToyChoicerFactory
    {
        UniTask<(ToyChoicerMediator, ToyChoicerStateMachine)> SpawnAsync(AssetReferenceGameObject toyAsset1, 
            AssetReferenceGameObject toyAsset2, Vector3 position);
    }
}