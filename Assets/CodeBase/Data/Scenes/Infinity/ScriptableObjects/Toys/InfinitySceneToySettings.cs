using CodeBase.Data.General.Constants;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.Scenes.Infinity.ScriptableObjects.Toys
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(InfinitySceneToySettings),
        fileName = nameof(InfinitySceneToySettings))]
    public class InfinitySceneToySettings : ScriptableObject
    {
        [SerializeField] 
        private AssetReferenceGameObject[] _toys;

        public AssetReferenceGameObject[] Toys => _toys;
    }
}