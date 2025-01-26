using CodeBase.Logic.General.Systems.ToyChoicer;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Toys
{
    public interface IToyChoicerFactory
    {
        UniTask<ToyChoicer> SpawnAsync(AssetReferenceGameObject toyAsset1, 
            AssetReferenceGameObject toyAsset2, Vector3 position);
    }
}